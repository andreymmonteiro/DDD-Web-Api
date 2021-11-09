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
    public class WhenDeleteMunicipio :BaseMunicipio
    {
        [Fact(DisplayName ="Can Delete Municipio.")]
        public async Task CAN_DELETE_MUNICIPIO()
        {
            serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(setup => setup.Delete(municipio.Id)).ReturnsAsync(true);
            service = serviceMock.Object;
            var resultDelete = await service.Delete(municipio.Id);
            Assert.True(resultDelete);
        }
    }
}
