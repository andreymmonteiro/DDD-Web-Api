using Data;
using Data.Context;
using Data.Implementations;
using Data.Repositories;
using Domain.Interfaces;
using Domain.Repository;
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
        public static string database { get; set; }
        private static IDatabaseImplementation db;
        public static void ConfigureDependencyInjection(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            CreateServiceConnectionDb(serviceCollection);
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();
            serviceCollection.AddTransient<IUfRepository, UfImplementations>();
            serviceCollection.AddTransient<IMunicipioRepository, MunicipioImplementations>();
            serviceCollection.AddTransient<ICepRepository, CepImplementations>();
            
        }
        private static void CreateServiceConnectionDb(IServiceCollection serviceCollection) 
        {

            switch (database) 
            {
                case "sqlserver" :
                    db = new DatabaseImplementationSqlServer(stringConnection, serviceCollection);
                    break;
                default:
                    db = new DatabaseImplementationMySql(stringConnection, serviceCollection);
                    break;
            }
            db.AddDbContext();
        }
    }
}
