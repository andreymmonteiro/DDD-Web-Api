using Domain.Dtos.Municipio;
using Domain.Interfaces.Services.Municipio;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Service.Test.Municipio
{
    public class WhenGetMunicipio : BaseMunicipio
    {
        [Fact(DisplayName ="Can Get Municipio.")]
        public async Task CAN_GET_MUNICIPIO() 
        {
            serviceMock = new Mock<IMunicipioService>();
            await Get();
            await GetAll();
        }
        private async Task Get() 
        {
            serviceMock.Setup(setup => setup.Get(municipio.Id)).ReturnsAsync(municipio);
            service = serviceMock.Object;
            var resultGet = await service.Get(municipio.Id);
            Test(municipio,resultGet);
        }
        private async Task GetAll() 
        {
            serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(setup => setup.GetAll()).ReturnsAsync(municipios);
            service = serviceMock.Object;
            var resultGet = await service.GetAll();
            for (int i = 0; i < municipios.Count(); i++)
                Test(municipios.OrderBy(order => order.Name).ToList()[i], resultGet.OrderBy(order => order.Name).ToList()[i]);
        }
        private void Test(MunicipioDto expected, MunicipioDto actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.UfId, actual.UfId);
            Assert.Equal(expected.CodigoIbge, actual.CodigoIbge);
        }

    }
}
