using application.Controllers;
using application.Header;
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
using System;
using System.Collections.Generic;

namespace application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void CreateTokenConfiguration(IServiceCollection services)
        {
            var refreshTokenConfiguration = new RefreshTokenConfiguration();
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);
            var tokenConfiguration = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(Configuration.GetSection("TokenConfigurations")).Configure(tokenConfiguration);
            new ConfigureFromConfigurationOptions<RefreshTokenConfiguration>(Configuration.GetSection("RefreshTokenConfiguration")).Configure(refreshTokenConfiguration);
            services.AddSingleton(tokenConfiguration);
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
            var config = new AutoMapper.MapperConfiguration(map =>
            {
                map.AddProfile(new DtoToModelProfile());
                map.AddProfile(new EntityToDtoProfile());
                map.AddProfile(new ModelToEntityProfile());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureService.ConfigureDependencyInjection(services);
            ConfigureRepository.stringConnection = Configuration.GetConnectionString("Default");
            ConfigureRepository.database = Configuration.GetSection("DataBase").GetSection("DATABASE").Value;
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
                //Inclui definições para o botão de autorização do Swagger
                //Aqui é parametrizado como será a autorização e os detalhes do botão
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Informe o Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                //Aqui é onde ele aplica as configurações de auth para que seja possível utilizar os métodos da controller
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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
            if (Configuration.GetSection("DataBase").GetSection("MIGRATION").Value.ToLower() == "APPLY".ToLower())
                using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                                                            .CreateScope())
                using (var context = service.ServiceProvider.GetService<MyContext>())
                    context.Database.Migrate();
        }
    }
}
