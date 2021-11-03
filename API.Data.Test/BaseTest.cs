using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace API.Data.Test 
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }
    }
    public class DbTest : IDisposable
    {
        private string databaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-",string.Empty)}";
        public ServiceProvider ServiceProvider { get; private set; }

        public DbTest() 
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(options =>
                options.UseMySql($"Persist Security Info=True;Server=localhost;Port=3306;Database={databaseName};Uid=root;Pwd=masterkey", ServerVersion.Parse("5.7-mysql")),
                ServiceLifetime.Transient);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureCreated();
            }
        }
        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}

