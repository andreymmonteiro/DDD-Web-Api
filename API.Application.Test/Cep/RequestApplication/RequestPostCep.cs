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
    public class RequestPostCep : BaseCepRequest
    {
        private CepDtoCreate cepDtoCreate;
        [Fact(DisplayName ="Can Post Test Application.")]
        public async Task CAN_POST_CEP() 
        {
            cepDtoCreate = new CepDtoCreate() 
            {
                Numero = numero,
                Cep = cep,
                Logradouro = logradouro,
                MunicipioId = municipioId
            };
            CepDtoCreateResult cepDtoCreateResult = new CepDtoCreateResult() 
            {
                Id = id,
                Numero = numero,
                Cep = cep,
                Logradouro = logradouro,
                MunicipioId = municipioId
            };
            serviceMock.Setup(setup => setup.Post(cepDtoCreate)).ReturnsAsync(cepDtoCreateResult);
            InitializeController();
            var resultController = await controller.Post(cepDtoCreate);
            Assert.True(resultController is CreatedResult);
            var resultContentController = (CepDtoCreateResult)((CreatedResult)resultController).Value;
            Assert.NotNull(resultContentController);
            Assert.False(resultContentController.Id == Guid.Empty);
            Assert.Equal(cepDtoCreate.Logradouro, resultContentController.Logradouro);
            Assert.Equal(cepDtoCreate.MunicipioId, resultContentController.MunicipioId);
            Assert.Equal(cepDtoCreate.Numero, resultContentController.Numero);
            Assert.Equal(cepDtoCreate.Cep, resultContentController.Cep);
        }
        private async Task BadRequest() 
        {
            controller.ModelState.AddModelError("Numero", "Error post Test Application.");
            var resultController = await controller.Post(cepDtoCreate);
            Assert.True(resultController is BadRequestObjectResult);
        }
    }
}
