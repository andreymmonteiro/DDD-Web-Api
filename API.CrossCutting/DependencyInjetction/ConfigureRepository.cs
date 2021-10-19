using Data.Context;
using Data.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DependencyInjetction
{
    public class ConfigureRepository
    {
        public static string stringConnection { get; set; }
        public static void ConfigureDependencyInjection(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddDbContext<MyContext>(
                options => options.UseMySql(stringConnection, ServerVersion.Parse("5.7-mysql"))
                );
        }
    }
}
