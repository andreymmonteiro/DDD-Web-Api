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
    public class RequestPutCep : BaseCepRequest
    {
        private CepDtoUpdate cepDtoUpdate;
        [Fact(DisplayName ="Can Put Cep Test Application.")]
        public async Task CAN_PUT_CEP() 
        {
            cepDtoUpdate = new CepDtoUpdate() 
            {
                Id = id,
                Logradouro = logradouro,
                Numero =numero,
                Cep = cep,
                MunicipioId = municipioId
            };
            CepDtoUpdateResult cepDtoUpdateResult = new CepDtoUpdateResult() 
            {
                Id = id,
                Logradouro = logradouro,
                Numero = numero,
                Cep = cep,
                MunicipioId = municipioId,
                UpdateAt = updateAt
            };
            serviceMock.Setup(setup => setup.Put(cepDtoUpdate)).ReturnsAsync(cepDtoUpdateResult);
            InitializeController();
            var resultController = await controller.Put(cepDtoUpdate);
            Assert.True(resultController is OkObjectResult);
            var resultContentController = (CepDtoUpdateResult)((OkObjectResult)resultController).Value;
            Assert.False(resultContentController.Id == Guid.Empty);
            Assert.Equal(updateAt,resultContentController.UpdateAt);
            Assert.Equal(cepDtoUpdate.Id, resultContentController.Id);
            await BadRequest();
        }
        private async Task BadRequest() 
        {
            controller.ModelState.AddModelError("Id", "Error test Application.");
            var resultController = await controller.Put(cepDtoUpdate);
            Assert.True(resultController is BadRequestObjectResult);
        }
    }
}
