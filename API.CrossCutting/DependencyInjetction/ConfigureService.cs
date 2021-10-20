using Data.Repositories;
using Domain.Interfaces;
using Domain.Interfaces.Services.Users;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
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
        }
    }
}
