using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        private string stringConnection { get; set; }

        public MyContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=127.0.0.1;Port=3306;Database=dbAPI;Uid=root;Pwd=masterkey";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(connectionString,
                ServerVersion.Parse("5.7-mysql"),
                options => options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));

            return new MyContext(optionsBuilder.Options);
        }
        //public ContextFactory(string stringConnection) => this.stringConnection = stringConnection;

        

    }
}
