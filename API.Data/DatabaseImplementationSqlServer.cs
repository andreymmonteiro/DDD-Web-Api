using Data.Context;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Data
{
    public class DatabaseImplementationSqlServer : IDatabaseImplementation
    {
        private readonly string ConnectionString;
        private IServiceCollection services;

        public DatabaseImplementationSqlServer(string connectionString, IServiceCollection services)
        {
            ConnectionString = connectionString;
            this.services = services;
        }

        public void AddDbContext()
        {
            services.AddDbContext<MyContext>(
                options => options.UseSqlServer(ConnectionString)
                ) ;
        }
    }
}
