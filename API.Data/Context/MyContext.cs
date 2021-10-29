using Data.Mapping;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UsersEntity> User { get; set;  }
        
        public MyContext(DbContextOptions<MyContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            //Ao criar o contexto ele pega essa entidade e faz o mapeamento
            //Configura todos os aspectos dessa tabela e suas características
            base.OnModelCreating(modelbuilder);
            modelbuilder.Entity<UsersEntity>(new UserMap().Configure);
            modelbuilder.Entity<UsersEntity>().HasData(
                new UsersEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "Baki Hanma",
                    Email = "baki@hanma.com",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                }
            );
        }
    }
}
