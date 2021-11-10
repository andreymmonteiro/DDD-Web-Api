using Domain.Dtos.Municipio;
using Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Application.Test.Municipio.RequestApplication
{
    public class RequestGetMunicipio : BaseMunicipioRequest
    {
        private MunicipioDto municipioDto;

        [Fact(DisplayName ="Can GET Municipio.")]
        public async Task CAN_GET_MUNICIPIO()
        {
            await Get();
            await GetAll();
        }
        private async Task Get() 
        {
            municipioDto = new MunicipioDto() 
            {
                Id = id,
                Name = name,
                CodigoIbge = codigoIbge,
                UfId = ufId
            };
            serviceMock.Setup(setup => setup.Get(id)).ReturnsAsync(municipioDto);
            InitializeController();
            var resultGet = await controller.Get(id);
            Assert.NotNull(resultGet);
            Assert.True(resultGet is OkObjectResult);
            var resultMunicipioDto = (MunicipioDto)((OkObjectResult)resultGet).Value;
            Test(municipioDto, resultMunicipioDto);

        }
        private async Task GetAll() 
        {
            List<MunicipioDto> municipioDtos = new List<MunicipioDto>();
            for (int i = 0; i < numericRandom; i++)
                municipioDtos.Add(new MunicipioDto() 
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.StreetAddress(),
                    CodigoIbge = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = ufId
                });
            serviceMock.Setup(setup => setup.GetAll()).ReturnsAsync(municipioDtos);
            InitializeController();
            var resultGet = await controller.Get();
            Assert.True(resultGet is OkObjectResult);
            var resultMunicipioDto = (List<MunicipioDto>)((OkObjectResult)resultGet).Value;
            for (int i = 0; i < municipioDtos.Count(); i++)
                Test(municipioDtos.OrderBy(order => order.Id).ToList()[i], resultMunicipioDto.OrderBy(order => order.Id).ToList()[i]);
        }
        private void Test(MunicipioDto expected, MunicipioDto actual) 
        {
            Assert.False(actual.Id == Guid.Empty);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.UfId, actual.UfId);
            Assert.Equal(expected.CodigoIbge, actual.CodigoIbge);
        }
    }
}
