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
    public class UfGet : IClassFixture<DbTest>
    {
        private ServiceProvider serviceProvider;
        public UfGet(DbTest dbTest)
        {
            serviceProvider = dbTest.ServiceProvider;
        }
        [Fact(DisplayName ="Can Get Uf.")]
        public async Task CAN_GET_UF() 
        {
            using (var context = serviceProvider.GetService<MyContext>()) 
            {
                UfImplementations repository = new UfImplementations(context);
                UfEntity uf = new UfEntity()
                {
                    Id = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                    Sigla = "SP",
                    Name = "São Paulo"
                };
                var ufExist = await repository.ExistAsync(uf.Id);
                Assert.True(ufExist);
                var ufGet = await repository.SelectAsync(uf.Id);
                Assert.NotNull(ufGet);
                Assert.Equal(uf.Id, ufGet.Id);
                Assert.Equal(uf.Name, ufGet.Name);
                Assert.Equal(uf.Sigla, ufGet.Sigla);

                var allUfs = await repository.SelectAsync();
                Assert.NotNull(allUfs);
                Assert.True(allUfs.Count() == 27);
            }
        }
    }
}
