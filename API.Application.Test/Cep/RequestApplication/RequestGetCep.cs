using Domain.Dtos.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Application.Test.Cep.RequestApplication
{
    public class RequestGetCep : BaseCepRequest
    {
        private CepDto cepDto;
        [Fact(DisplayName ="Can Get Cep Test Application.")]
        public async Task CAN_GET_CEP() 
        {
            await GetbyCep();
            await GetById();
            await GetAll();
        }
        private async Task GetbyCep() 
        {
            cepDto = new CepDto()
            {
                Id = id,
                Cep = cep,
                Logradouro = logradouro,
                MunicipioId = municipioId,
                Numero = numero
            };
            serviceMock.Setup(setup => setup.Get(cep)).ReturnsAsync(cepDto);
            InitializeController();
            var resultController = await controller.Get(cep);
            Assert.NotNull(resultController);
            Assert.True(resultController is OkObjectResult);
            var resultContentController = (CepDto)((OkObjectResult)resultController).Value;
            Test(cepDto, resultContentController);
            await BadRequestByCep();
        }
        private void Test(CepDto expected, CepDto actual) 
        {
            Assert.NotNull(actual);
            Assert.False(actual.Id == Guid.Empty);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Numero, actual.Numero);
            Assert.Equal(expected.Cep, actual.Cep);
            Assert.Equal(expected.MunicipioId, actual.MunicipioId);
        }
        private async Task GetById() 
        {
            serviceMock.Setup(setup => setup.Get(id)).ReturnsAsync(cepDto);
            InitializeController();
            var resultController = await controller.Get(id);
            Assert.True(resultController is OkObjectResult);
            var resultContentController = (CepDto)((OkObjectResult)resultController).Value;
            Test(cepDto,resultContentController);
            await BadRequestGetById();
        }
        private async Task GetAll() 
        {
            List<CepDto> cepDtos = new List<CepDto>();
            for (int i = 0; i < numeriRandom; i++)
                cepDtos.Add(new CepDto() 
                {
                    Id = Guid.NewGuid(),
                    MunicipioId= Guid.NewGuid(),
                    Cep= "95050812",
                    Logradouro = Faker.Address.StreetName(),
                    Numero = Faker.Address.UkPostCode()
                });
            serviceMock.Setup(setup => setup.GetAll()).ReturnsAsync(cepDtos);
            InitializeController();
            var resultController = await controller.Get();
            Assert.True(resultController is OkObjectResult);
            var resultContentController = (List<CepDto>)((OkObjectResult)resultController).Value;
            for (int i = 0; i < numeriRandom; i++)
                Test(cepDtos.OrderBy(order => order.Id).ToList()[i], resultContentController.OrderBy(order => order.Id).ToList()[i]);
            await BadRequestGetAll();
            
        }
        private async Task BadRequestByCep() 
        {
            controller.ModelState.AddModelError("Id", "Error test Application.");
            var resultController = await controller.Get(cep);
            Assert.True(resultController is BadRequestObjectResult);
        }
        private async Task BadRequestGetById() 
        {
            controller.ModelState.AddModelError("Id", "Error test Appplication.");
            var resultController = await controller.Get(id);
            Assert.True(resultController is BadRequestObjectResult);
        }
        private async Task BadRequestGetAll() 
        {
            controller.ModelState.AddModelError("Id","Error test Application.");
            var resultController = await controller.Get();
            Assert.True(resultController is BadRequestObjectResult);
        }
        
    }
}
