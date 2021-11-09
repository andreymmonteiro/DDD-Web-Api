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
    public class CepCrudComplete : IClassFixture<DbTest>
    {
        private ServiceProvider serviceProvider;
        private MyContext? myContext;
        private CepImplementations repository;
        private MunicipioEntity municipio;
        private CepEntity cep = new CepEntity()
        {
            Id = Guid.NewGuid(),
            Cep = Faker.RandomNumber.Next(10000000, 99999999).ToString(),
            Logradouro = Faker.Address.StreetName(),
            Numero = Faker.Address.UkPostCode()
        };

        public CepCrudComplete(DbTest dbTest)
        {
            serviceProvider = dbTest.ServiceProvider;
            myContext = serviceProvider.GetService<MyContext>();
            repository = new CepImplementations(myContext);
        }
        [Fact(DisplayName ="Can Crud Cep.")]
        public async Task CAN_CRUD_CEP() 
        {
            await CreateMunicipio();
            await Create();
            await Update();
            await Get();
            await GetByCep();
            await Delete();
            await GetAll(10);
            myContext.Dispose();
        }
        private async Task Delete() 
        {
            var resultDelete = await repository.DeleteAsync(cep.Id);
            Assert.True(resultDelete);
        }
        private async Task GetAll(int count) 
        {
            List<CepEntity> ceps = new List<CepEntity>();
            for(int i=0; i < count; i++) 
            {
                cep = new CepEntity()
                {
                    Id = Guid.NewGuid(),
                    Cep = Faker.RandomNumber.Next(10000000, 99999999).ToString(),
                    Logradouro = Faker.Address.StreetName(),
                    Numero = Faker.Address.UkPostCode(),
                    MunicipioId = municipio.Id
                };
                await Create(false);
                ceps.Add(cep);
            }
            var resultGetAll = await repository.SelectAsync();
            for (int i = 0; i < count; i++) 
            {
                cep = ceps.OrderBy(order => order.Cep).ToList()[i];
                await Test(resultGetAll.OrderBy(order => order.Cep).ToList()[i]);
            }
        }
        private async Task Get() 
        {
            var resultGet = await repository.SelectAsync(cep.Id);
            await Test(resultGet);
            Assert.Equal(cep.Id, resultGet.Id);
        }
        private async Task GetByCep() 
        {
            var resultGet = await repository.SelectAsync(cep.Cep);
            await Test(resultGet);
            Assert.Equal(cep.Id, resultGet.Id);
        }
        private async Task Update() 
        {
            cep.Numero = "S/N";
            cep.Logradouro = "Robaro";
            var resultUpdate = await repository.UpdateAsync(cep);
            await Test(resultUpdate);
        }
        private async Task Test(CepEntity result) 
        {
            Assert.NotNull(result);
            Assert.False(result.Id == Guid.Empty);
            Assert.Equal(cep.Logradouro, result.Logradouro);
            Assert.Equal(cep.Numero, result.Numero);
            Assert.Equal(cep.Cep, result.Cep);
            Assert.Equal(cep.MunicipioId, result.MunicipioId);
        }
        private async Task Create(bool test = true) 
        {
            cep.MunicipioId = municipio.Id;
            var resultCreate = await repository.InsertAsync(cep);
            if(test)
               await Test(resultCreate);
        }
        private async Task CreateMunicipio() 
        {
            MunicipioImplementations repositoryMunicipio = new MunicipioImplementations(myContext);
            municipio = new MunicipioEntity()
            {
                Name = Faker.Address.City(),
                UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                CodigoIbge = Faker.RandomNumber.Next(1000000, 9999999)
            };
            var result = await repositoryMunicipio.InsertAsync(municipio);
            Assert.NotNull(result);
        }
    }
}
