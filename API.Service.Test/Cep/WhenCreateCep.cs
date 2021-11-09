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
    public class WhenCreateCep : BaseCep
    {
        [Fact(DisplayName ="Can Create Cep.")]
        public async Task CAN_CREATE_CEP()
        {
            serviceMock = new Mock<ICepService>();
            serviceMock.Setup(setup => setup.Post(cepDtoCreate)).ReturnsAsync(cepDtoCreateResult);
            service = serviceMock.Object;
            var resultPost = await service.Post(cepDtoCreate);
            Test(cepDtoCreateResult, resultPost);
        }
        private void Test(CepDtoCreateResult expected, CepDtoCreateResult actual)
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
