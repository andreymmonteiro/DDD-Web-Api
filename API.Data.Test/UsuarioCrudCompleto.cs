using Data.Context;
using Data.Implementations;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Data.Test
{
    public class UsuarioCrudCompleto : IClassFixture<DbTest>
    {
        private ServiceProvider serviceProvider;

        public UsuarioCrudCompleto(DbTest DbTest)
        {
            this.serviceProvider = DbTest.ServiceProvider;
        }
        [Fact(DisplayName ="CRUD de usuário")]
        [Trait("CRUD", "UsersEntity")]
        public async Task CAN_CRUD() 
        {
            using (var context = serviceProvider.GetService<MyContext>()) 
            {
                UserImplementation user = new UserImplementation(context);
                UsersEntity entity = new UsersEntity()
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()                    
                };
                await CreateTest(entity, user);
                await UpdateTest(entity, user);
                await SelectTest(entity, user);
                await DeleteTest(entity.Id, user);
                await FindByLoginTest("baki@hanma.com", user);
            }
        }
        private void Test(UsersEntity entity, UsersEntity registroCriado) 
        {
            Assert.NotNull(registroCriado);
            Assert.Equal(entity.Email, registroCriado.Email);
            Assert.Equal(entity.Name, registroCriado.Name);
            Assert.False(registroCriado.Id == Guid.Empty);
        }
        private async Task CreateTest(UsersEntity entity, UserImplementation user) 
        {
            var registroCriado = await user.InsertAsync(entity);
            Test(entity, registroCriado);
        }
        private async Task UpdateTest(UsersEntity entity, UserImplementation user) 
        {
            entity.Name = Faker.Name.First();
            var registroAtualizado = await user.UpdateAsync(entity);
            Test(entity, registroAtualizado);
        }
        private async Task SelectTest(UsersEntity usersEntity, UserImplementation user)
        {
            var allUsers =  await user.SelectAsync();
            Assert.NotNull(allUsers);
            var oneUser = await user.SelectAsync(usersEntity.Id);
            Assert.NotNull(oneUser);
            Test(usersEntity,oneUser);
        }
        private async Task DeleteTest(Guid Id, UserImplementation user) 
        {
            var idRemoved = await user.DeleteAsync(Id);
            Assert.True(idRemoved);
        }

        private async Task FindByLoginTest(string username, UserImplementation user)
        {
            var result = await user.FindByLogin(username);
            Assert.NotNull(result);
            Assert.Equal("Baki Hanma", result.Name);
            Assert.Equal("baki@hanma.com", result.Email);
        }
    }
}
