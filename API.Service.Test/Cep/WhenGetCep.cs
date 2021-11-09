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
    public class WhenGetCep : BaseCep
    {
        [Fact(DisplayName ="Can Get Cep.")]
        public async Task CAN_GET_CEP() 
        {
            await GetByCep();
            await Get();
            await GetAll();
        }
        private async Task GetAll()
        {
            serviceMock = new Mock<ICepService>();
            serviceMock.Setup(setup => setup.GetAll()).ReturnsAsync(ceps);
            service = serviceMock.Object;
            var resultGet = await service.GetAll();
            for (int i = 0; i < ceps.Count(); i++)
                Test(ceps.OrderBy(order => order.Id).ToList()[i], resultGet.OrderBy(order => order.Id).ToList()[i]);
        }
        private async Task Get() 
        {
            serviceMock = new Mock<ICepService>();
            serviceMock.Setup(setup => setup.Get(id)).ReturnsAsync(cepDto);
            service = serviceMock.Object;
            var resultGet = await service.Get(id);
            Test(cepDto, resultGet);
        }
        private async Task GetByCep() 
        {
            serviceMock = new Mock<ICepService>();
            serviceMock.Setup(setup => setup.Get(cep)).ReturnsAsync(cepDto);
            service = serviceMock.Object;
            var resultGet = await service.Get(cep);
            Test(cepDto, resultGet);
        }
        private void Test(CepDto expected, CepDto actual) 
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Numero, actual.Numero);
            Assert.Equal(expected.MunicipioId, actual.MunicipioId);
            Assert.Equal(expected.Logradouro, actual.Logradouro);
        }
    }
}
