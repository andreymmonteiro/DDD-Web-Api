using Data.Mapping;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UserEntity> User { get; set;  }
        
        public MyContext(DbContextOptions<MyContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            //Ao criar o contexto ele pega essa entidade e faz o mapeamento
            //Configura todos os aspectos dessa tabela e suas características
            base.OnModelCreating(modelbuilder);
            modelbuilder.Entity<UserEntity>(new UserMap().Configure);
        }
    }
}
