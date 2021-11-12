using Domain.Dtos.Municipio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Integration.Test.Municipio
{
    public class WhenRequestCRUDMunicipio : BaseIntegration
    {
        private MunicipioDtoCreate municipioDtoCreate;
        private MunicipioDtoUpdate municipioDtoUpdate;
        [Fact(DisplayName ="Can CRUD on integration Municipio.")]
        public async Task CAN_CRUD_MUNICIPIO() 
        {
            await AddToken();
            await Create(true);
        }
        private async Task Create(bool isTest = false) 
        {
            if(isTest)
                municipioDtoCreate = new MunicipioDtoCreate()
                {
                    Name = Faker.Address.City(),
                    UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                    CodigoIbge = Faker.RandomNumber.Next(1000000, 9999999)
                };

            var resultHttp = await PostDataAsync(municipioDtoCreate, $"{hostApi}municipio", client);
            Assert.Equal(HttpStatusCode.Created, resultHttp.StatusCode);
            var resultHttpContent = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(await resultHttp.Content.ReadAsStringAsync());
            if (isTest) 
            {
                Assert.NotNull(resultHttpContent);
                Assert.False(resultHttpContent.Id == Guid.Empty);
                Assert.Equal(municipioDtoCreate.Name, resultHttpContent.Name);
                Assert.Equal(municipioDtoCreate.UfId, resultHttpContent.UfId);
                Assert.Equal(municipioDtoCreate.CodigoIbge, resultHttpContent.CodigoIbge);
                await Update(resultHttpContent.Id);
            }
        }
        private async Task Update(Guid Id) 
        {
            municipioDtoUpdate = new MunicipioDtoUpdate() 
            {
                Id = Id,
                CodigoIbge = Faker.RandomNumber.Next(1000000, 9999999),
                UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                Name = Faker.Address.City()
            };
            var resultHttp = await PutDataAsync($"{hostApi}municipio", municipioDtoUpdate, client);
            Assert.Equal(HttpStatusCode.OK, resultHttp.StatusCode);
            var resultHttpContent = JsonConvert.DeserializeObject<MunicipioDtoUpdateResult>(await resultHttp.Content.ReadAsStringAsync());
            Assert.NotNull(resultHttpContent);
            Assert.False(resultHttpContent.Id == Guid.Empty);
            Assert.Equal(municipioDtoUpdate.Id, resultHttpContent.Id);
            Assert.Equal(municipioDtoUpdate.Name, resultHttpContent.Name);
            Assert.Equal(municipioDtoUpdate.UfId, resultHttpContent.UfId);
            Assert.Equal(municipioDtoUpdate.CodigoIbge, resultHttpContent.CodigoIbge);

            await GetById();
            await Delete();
            await GetAll();
        }
        private async Task GetById() 
        {
            var resultHttp = await GetDataAsync($"{hostApi}municipio/{municipioDtoUpdate.Id}",client);
            Assert.Equal(HttpStatusCode.OK, resultHttp.StatusCode);
            var resultHttpContent = JsonConvert.DeserializeObject<MunicipioDto>(await resultHttp.Content.ReadAsStringAsync());
            Assert.NotNull(resultHttp);
            Assert.Equal(municipioDtoUpdate.Id, resultHttpContent.Id);
            Assert.Equal(municipioDtoUpdate.Name, resultHttpContent.Name);
            Assert.Equal(municipioDtoUpdate.UfId, resultHttpContent.UfId);
            Assert.Equal(municipioDtoUpdate.CodigoIbge, resultHttpContent.CodigoIbge);
        }
        private async Task GetAll() 
        {
            List<MunicipioDto> municipiosDto = new List<MunicipioDto>();
            for (int i = 0; i < numericRandon; i++) 
            {
                municipioDtoCreate = new MunicipioDtoCreate()
                {
                    Name = Faker.Address.City(),
                    UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                    CodigoIbge = Faker.RandomNumber.Next(1000000, 9999999)
                };
                municipiosDto.Add(new MunicipioDto()
                {
                   Name = municipioDtoCreate.Name,
                   UfId = municipioDtoCreate.UfId,
                   CodigoIbge = municipioDtoCreate.CodigoIbge
                });
                await Create();
            }
                
            var resultHttp = await GetDataAsync($"{hostApi}municipio",client);
            Assert.Equal(HttpStatusCode.OK,resultHttp.StatusCode);
            var resultHttpContent = JsonConvert.DeserializeObject<List<MunicipioDto>>(await resultHttp.Content.ReadAsStringAsync());
            Assert.NotNull(resultHttpContent);
            Assert.False(resultHttpContent.Count() == 0);
            for (int i = 0; i < resultHttpContent.Count(); i++)
                Test(municipiosDto.OrderBy(order => order.Name).ToList()[i],resultHttpContent.OrderBy(order => order.Name).ToList()[i]);
        }
        private void Test(MunicipioDto expected, MunicipioDto actual) 
        {
            Assert.NotNull(actual);
            Assert.False(actual.Id == Guid.Empty);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.UfId, actual.UfId);
            Assert.Equal(expected.CodigoIbge, actual.CodigoIbge);
        }
        private async Task Delete() 
        {
            var resultHttp = await DeleteDataAsync($"{hostApi}municipio/{municipioDtoUpdate.Id}",client);
            Assert.Equal(HttpStatusCode.OK, resultHttp.StatusCode);
            var resultHttpContent = JsonConvert.DeserializeObject<bool>(await resultHttp.Content.ReadAsStringAsync());
            Assert.True(resultHttpContent);
        }
    }
}
