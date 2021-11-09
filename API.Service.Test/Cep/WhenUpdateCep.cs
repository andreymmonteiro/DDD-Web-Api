using Domain.Dtos.Cep;
using Domain.Interfaces.Services.Cep;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Service.Test.Cep
{
    public class WhenUpdateCep : BaseCep
    {
        [Fact(DisplayName ="Can update Cep.")]
        public async Task CAN_UPDATE_CEP()
        {
            serviceMock = new Mock<ICepService>();
            serviceMock.Setup(setup => setup.Put(cepDtoUpdate)).ReturnsAsync(cepDtoUpdateResult);
            service = serviceMock.Object;
            var resultPut = await service.Put(cepDtoUpdate);
            Test(cepDtoUpdateResult, resultPut);
        }
        private void Test(CepDtoUpdateResult expected, CepDtoUpdateResult actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Logradouro, actual.Logradouro);
            Assert.Equal(expected.Cep, actual.Cep);
            Assert.Equal(expected.MunicipioId, actual.MunicipioId);
            Assert.Equal(expected.Numero, actual.Numero);
        }
    }
}
