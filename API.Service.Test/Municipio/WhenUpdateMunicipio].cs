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
    public class WhenUpdateMunicipio_ : BaseMunicipio
    {
        [Fact(DisplayName ="Can Update Municipio.")]
        public async Task CAN_UPDATE_MUNICIPIO()
        {
            serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(setup => setup.Put(municipioDtoUpdate)).ReturnsAsync(municipioDtoUpdateResult);
            service = serviceMock.Object;
            var resultPut = await service.Put(municipioDtoUpdate);
            Test(municipioDtoUpdateResult, resultPut);
        }
        private void Test(MunicipioDtoUpdateResult expected, MunicipioDtoUpdateResult actual)
        {
            Assert.NotNull(actual);
            Assert.False(actual.Id == Guid.Empty);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.UfId, actual.UfId);
            Assert.Equal(expected.CodigoIbge, actual.CodigoIbge);
        }
    }
}
