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
    public class WhenCreateMunicipio : BaseMunicipio
    {
        [Fact(DisplayName ="Can Create Municipio.")]
        public async Task CAN_CREATE_MUNICIPIO()
        {
            serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(setup => setup.Post(municipioDtoCreate)).ReturnsAsync(municipioDtoCreateResult);
            service = serviceMock.Object;
            var resultPost = await service.Post(municipioDtoCreate);
            Test(municipioDtoCreateResult,resultPost);
        }
        private void Test(MunicipioDtoCreateResult expected, MunicipioDtoCreateResult actual)
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
