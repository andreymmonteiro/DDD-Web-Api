using Data.Repositories;
using Domain.Interfaces;
using Domain.Interfaces.Services.Municipio;
using Domain.Interfaces.Services.Token;
using Domain.Interfaces.Services.Uf;
using Domain.Interfaces.Services.Users;
using Domain.Repository;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using Service.Services.TokenServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DependencyInjetction
{
    public class ConfigureService
    {
        public static void ConfigureDependencyInjection(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUsersService, UserService>();
            serviceCollection.AddTransient<ILoginService, LoginService>();
            serviceCollection.AddSingleton<ITokenService, TokenService>();
            serviceCollection.AddTransient<IUfService, UfService>();
            serviceCollection.AddTransient<IMunicipioService, MunicipioService>();
        }
    }
}
