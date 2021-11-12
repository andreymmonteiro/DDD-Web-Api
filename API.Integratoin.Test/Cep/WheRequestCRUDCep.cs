using Domain.Dtos.Cep;
using Domain.Dtos.Municipio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Integration.Test.Cep
{
    public class WheRequestCRUDCep : BaseIntegration
    {
        private CepDtoCreateResult cepDtoCreateResult;
        private CepDtoUpdateResult cepDtoUpdateResult;
        private CepDto cepDto;
        private List<CepDto> cepsDto = new List<CepDto>();
        private MunicipioDtoCreateResult municipio;

        [Fact(DisplayName ="Can CRUD Cep Test Integration.")]
        public async Task CAN_CRUD_CEP() 
        {
            await AddToken();
            municipio = await CreateMunicipio();
            await Create(municipio, true);
            await Update();
            await Get();
            await GetbyCep();
            await Delete();
            await GetAll();
        }
        private async Task Delete() 
        {
            response = await DeleteDataAsync($"{hostApi}cep/{cepDtoUpdateResult.Id}",client);
            var resultHttpContent = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
            Assert.True(resultHttpContent);
        }
        private async Task Create(MunicipioDtoCreateResult municipio, bool isTest = false) 
        {
            CepDtoCreate cepDtoCreate = new CepDtoCreate()
            {
                Cep = "95050812",
                Logradouro = Faker.Address.StreetAddress(),
                Numero = Faker.Address.UkPostCode(),
                MunicipioId = municipio.Id
            };
            response = await PostDataAsync(cepDtoCreate, $"{hostApi}cep", client);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            cepDtoCreateResult = JsonConvert.DeserializeObject<CepDtoCreateResult>(await response.Content.ReadAsStringAsync());
            if (isTest)
            {
                Assert.NotNull(cepDtoCreateResult);
                Assert.Equal(cepDtoCreate.Logradouro, cepDtoCreateResult.Logradouro);
                Assert.Equal(cepDtoCreate.Numero, cepDtoCreateResult.Numero);
                Assert.Equal(cepDtoCreate.Cep, cepDtoCreateResult.Cep);
                Assert.Equal(cepDtoCreate.MunicipioId, cepDtoCreateResult.MunicipioId);
            }
            else
                cepsDto.Add(new CepDto() 
                {
                    Id = cepDtoCreateResult.Id,
                    Cep = cepDtoCreateResult.Cep,
                    Logradouro = cepDtoCreateResult.Logradouro,
                    MunicipioId = cepDtoCreateResult.MunicipioId,
                    Numero = cepDtoCreateResult.Numero
                });
        }
        private async Task GetAll() 
        {
            for (int i = 0; i < numericRandon; i++)
                await Create(municipio,false);
            response = await GetDataAsync($"{hostApi}cep",client);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var resultHttpContent = JsonConvert.DeserializeObject<List<CepDto>>(await response.Content.ReadAsStringAsync());
            for (int i = 0; i < resultHttpContent.Count(); i++)
                TestGet(cepsDto.OrderBy(order => order.Id).ToList()[i], resultHttpContent.OrderBy(order => order.Id).ToList()[i]);
        }
        private async Task GetbyCep() 
        {
            response = await GetDataAsync($"{hostApi}cep/{cepDto.Cep}",client);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var resultHttpContent = JsonConvert.DeserializeObject<CepDto>(await response.Content.ReadAsStringAsync());
            TestGet(cepDto,resultHttpContent);
        }
        private async Task Get() 
        {
            cepDto = new CepDto() 
            {
                Id = cepDtoUpdateResult.Id,
                Cep = cepDtoUpdateResult.Cep,
                Logradouro = cepDtoUpdateResult.Logradouro,
                MunicipioId = cepDtoUpdateResult.MunicipioId,
                Numero = cepDtoUpdateResult.Numero

            };
            response = await GetDataAsync($"{hostApi}cep/id/{cepDtoUpdateResult.Id}",client);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var resultHttpContent = JsonConvert.DeserializeObject<CepDto>(await response.Content.ReadAsStringAsync());
            TestGet(cepDto, resultHttpContent);
        }
        private async Task Update() 
        {
            CepDtoUpdate cepDtoUpdate = new CepDtoUpdate() 
            {
                Id = cepDtoCreateResult.Id,
                Cep = "95050812",
                Logradouro = Faker.Address.StreetAddress(),
                Numero = Faker.Address.UkPostCode(),
                MunicipioId = cepDtoCreateResult.MunicipioId
            };
            response = await PutDataAsync($"{hostApi}cep",cepDtoUpdate,client);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            cepDtoUpdateResult = JsonConvert.DeserializeObject<CepDtoUpdateResult>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(cepDtoUpdateResult);
            Assert.Equal(cepDtoUpdate.Logradouro, cepDtoUpdateResult.Logradouro);
            Assert.Equal(cepDtoUpdate.Numero, cepDtoUpdateResult.Numero);
            Assert.Equal(cepDtoUpdate.Cep, cepDtoUpdateResult.Cep);
            Assert.Equal(cepDtoUpdate.MunicipioId, cepDtoUpdateResult.MunicipioId);
        }
        private async Task<MunicipioDtoCreateResult> CreateMunicipio() 
        {
            MunicipioDtoCreate municipio = new MunicipioDtoCreate() 
            {
                UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                Name = Faker.Address.City(),
                CodigoIbge = Faker.RandomNumber.Next(1000000, 9999999)
            };
            response = await PostDataAsync(municipio, $"{hostApi}municipio", client);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            return JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(await response.Content.ReadAsStringAsync());
        }
        private void TestGet(CepDto expected, CepDto actual) 
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Logradouro, actual.Logradouro);
            Assert.Equal(expected.Numero, actual.Numero);
            Assert.Equal(expected.Cep, actual.Cep);
            Assert.Equal(expected.MunicipioId, actual.MunicipioId);
        }
    }
}
