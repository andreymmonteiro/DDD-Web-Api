using Domain.Dtos.Municipio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Integration.Test.Municipio
{
    public class WhenRequestCRUD : BaseIntegration
    {
        private MunicipioDtoCreate municipioDtoCreate;
        [Fact(DisplayName ="Can CRUD on integration Municipio.")]
        public async Task CAN_CRUD_MUNICIPIO() 
        {
            await AddToken();
            await Create();
        }
        private async Task Create() 
        {
            municipioDtoCreate = new MunicipioDtoCreate()
            {
                Name = Faker.Address.City(),
                UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                CodigoIbge = Faker.RandomNumber.Next(1000000, 9999999)
            };

            var resultHttp = await PostDataAsync(municipioDtoCreate, $"{hostApi}municipio", client);
            Assert.Equal(HttpStatusCode.Created, resultHttp.StatusCode);
        }
    }
}
