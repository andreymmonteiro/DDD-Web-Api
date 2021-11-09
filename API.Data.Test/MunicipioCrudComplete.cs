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
    public class MunicipioCrudComplete : IClassFixture<DbTest>
    {
        private ServiceProvider serviceProvider;
        private MunicipioEntity municipio = new MunicipioEntity() 
        {
            Name = Faker.Name.FullName(),
            UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
            CodigoIbge = Faker.RandomNumber.Next(1000000, 9999999)
        };
        public MunicipioCrudComplete(DbTest dbTest) 
        {
            serviceProvider = dbTest.ServiceProvider;
        }
        [Fact(DisplayName ="Can CRUD Municipio.")]
        public async Task CAN_CRUD_MUNICIPIO() 
        {
            using (var context = serviceProvider.GetService<MyContext>()) 
            {
                MunicipioImplementations repository = new MunicipioImplementations(context);

                await Create(repository, true);
                await Update(repository);
                await Get(repository);
                await Delete(repository);
                await GetAll(repository, 10);
                await GetIBGE(repository);
                await GetComplete(repository);
            }
        }
        private async Task GetComplete(MunicipioImplementations repository) 
        {
            var resultGet = await repository.GetComplete(municipio.Id);
            Test(resultGet);
            Assert.Equal(municipio.Id, resultGet.Id);
        }
        private async Task GetIBGE(MunicipioImplementations repository) 
        {
            var resultGet = await repository.GetComplete(municipio.CodigoIbge);
            Test(resultGet);
            Assert.Equal(municipio.Id, resultGet.Id);
        }
        private async Task Delete(MunicipioImplementations repository) 
        {
            var resultDelete = await repository.DeleteAsync(municipio.Id);
            Assert.True(resultDelete);
        }
        private void Test(MunicipioEntity result) 
        {
            Assert.NotNull(result);
            Assert.Equal(municipio.Name, result.Name);
            Assert.Equal(municipio.CodigoIbge, result.CodigoIbge);
            Assert.Equal(municipio.UfId, result.UfId);
            Assert.False(municipio.Id == Guid.Empty);
        }
        private async Task Get(MunicipioImplementations repository) 
        {
            var resultGet = await repository.SelectAsync(municipio.Id);
            Test(resultGet);
        }
        private async Task GetAll(MunicipioImplementations repository, int count) 
        {
            List<MunicipioEntity> municipios = new List<MunicipioEntity>();
            for(int i=0; i< count; i++) 
            {
                municipio = new MunicipioEntity()
                {
                    Name = Faker.Name.FullName(),
                    UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                    CodigoIbge = Faker.RandomNumber.Next(1000000, 9999999)
                };
                municipios.Add(municipio);
                await Create(repository);
            }
            var resultGetAll = await repository.SelectAsync();
            for(int i=0; i < count; i++) 
            {
                Assert.NotNull(resultGetAll);
                Assert.Equal(resultGetAll.OrderBy(order => order.Name).ToList()[i].Name, municipios.OrderBy(order => order.Name).ToList()[i].Name);
                Assert.Equal(resultGetAll.OrderBy(order => order.Name).ToList()[i].Id, municipios.OrderBy(order => order.Name).ToList()[i].Id);
                Assert.Equal(resultGetAll.OrderBy(order => order.Name).ToList()[i].UfId, municipios.OrderBy(order => order.Name).ToList()[i].UfId);
                Assert.Equal(resultGetAll.OrderBy(order => order.Name).ToList()[i].CodigoIbge, municipios.OrderBy(order => order.Name).ToList()[i].CodigoIbge);
            }
        }
        private async Task Create(MunicipioImplementations repository, bool test = false) 
        {
            var resultPost = await repository.InsertAsync(municipio);
            municipio.Id = resultPost.Id;
            if (test)
                Test(resultPost);
        }
        private async Task Update(MunicipioImplementations repository) 
        {
            municipio.Name = Faker.Address.City();
            municipio.CodigoIbge = Faker.RandomNumber.Next(1000000, 9999999);
            municipio.UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6");
            var resultPut = await repository.UpdateAsync(municipio);
            Test(resultPut);
            Assert.Equal(municipio.Id, resultPut.Id);
        }
    }
}
