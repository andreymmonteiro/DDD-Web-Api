using Data.Context;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Data
{
    public class DatabaseImplementationMySql : IDatabaseImplementation
    {
        private readonly string ConnectionString;
        private IServiceCollection services;
        public DatabaseImplementationMySql(string connectionString, IServiceCollection services)
        {
            ConnectionString = connectionString;
            this.services = services;
        }

        public void AddDbContext()
        {
            services.AddDbContext<MyContext>(
                options => options.UseMySql(ConnectionString, ServerVersion.Parse("5.7-mysql"))
                ) ;
        }
    }
}
