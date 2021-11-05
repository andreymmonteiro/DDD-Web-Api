using application.Controllers;
using AutoMapper;
using CrossCutting.DependencyInjetction;
using CrossCutting.Mappings;
using Data.Context;
using Domain.Models.Token;
using Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Service.Services.Mapper;
using System;
using System.Collections.Generic;

namespace application
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            this.environment = environment;
        }
        public IWebHostEnvironment environment { get; }
        public IConfiguration Configuration { get; }

        private void CreateTokenConfiguration(IServiceCollection services)
        {
            var refreshTokenConfiguration = new RefreshTokenConfiguration();
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);
            var tokenConfiguration = new TokenConfiguration();
            if (!environment.IsEnvironment("Testing"))
            {
                new ConfigureFromConfigurationOptions<TokenConfiguration>(Configuration.GetSection("TokenConfigurations")).Configure(tokenConfiguration);
            }
            else
            {
                tokenConfiguration = new TokenConfiguration()
                {
                    Audience = Environment.GetEnvironmentVariable("Audience"),
                    Issuer = Environment.GetEnvironmentVariable("Issuer"),
                    Seconds = Int32.Parse(Environment.GetEnvironmentVariable("Seconds"))
                };
            }

            services.AddSingleton(tokenConfiguration);
            new ConfigureFromConfigurationOptions<RefreshTokenConfiguration>(Configuration.GetSection("RefreshTokenConfiguration")).Configure(refreshTokenConfiguration);
            services.AddSingleton(refreshTokenConfiguration);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearer =>
            {
                var paramsValidation = bearer.TokenValidationParameters;
                paramsValidation.ValidAudience = tokenConfiguration.Audience;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidIssuer = tokenConfiguration.Issuer;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });
            services.AddAuthorization(auth => auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                                                                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                                                        .RequireAuthenticatedUser().Build()));
        }
        private void ConfigureAutoMapp(IServiceCollection services)
        {
            IMapper mapper = new AutoMapperFixture().GetMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureService.ConfigureDependencyInjection(services);
            if (environment.IsEnvironment("Testing")) 
            {
                Environment.SetEnvironmentVariable("DB_CONNECTION", "Persist Security Info=true;Server=localhost;Port=3306;Database=dbApiTest;Uid=root;Pwd=masterkey");
                Environment.SetEnvironmentVariable("DATABASE", "MYSQL");
                Environment.SetEnvironmentVariable("MIGRATION", "APPLY");
                Environment.SetEnvironmentVariable("Audience", "exampleAudience");
                Environment.SetEnvironmentVariable("Issuer", "exampleIssuer");
                Environment.SetEnvironmentVariable("Seconds", "120");
                ConfigureRepository.stringConnection = Environment.GetEnvironmentVariable("DB_CONNECTION");
                ConfigureRepository.database = Environment.GetEnvironmentVariable("DATABASE");

            }
            else 
            {
                ConfigureRepository.stringConnection = Configuration.GetConnectionString("Default");
                ConfigureRepository.database = Configuration.GetSection("DataBase").GetSection("DATABASE").Value;
            }

            ConfigureRepository.ConfigureDependencyInjection(services);

            CreateTokenConfiguration(services);

            ConfigureAutoMapp(services);

            services.AddControllers();

            OpenApiReference apiReference = new OpenApiReference()
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            };
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "application", Version = "v1" });
                //Inclui defini��es para o bot�o de autoriza��o do Swagger
                //Aqui � parametrizado como ser� a autoriza��o e os detalhes do bot�o
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Informe o Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                //Aqui � onde ele aplica as configura��es de auth para que seja poss�vel utilizar os m�todos da controller
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "application v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            var apply = "";
            if (!environment.IsEnvironment("Testing"))
                apply = Configuration.GetSection("DataBase").GetSection("MIGRATION").Value.ToLower();
            else
                apply = Environment.GetEnvironmentVariable("MIGRATION").ToLower();

            if (apply == "APPLY".ToLower())
                using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                                                            .CreateScope())
                using (var context = service.ServiceProvider.GetService<MyContext>())
                    context.Database.Migrate();
        }
    }
}
