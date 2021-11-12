using application.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Application.Test.Municipio.RequestApplication
{
    public class RequestDeleteMunicipio : BaseMunicipioRequest
    {
        [Fact(DisplayName ="Can delete Municipio Test Application.")]
        public async Task CAN_DELETE_MUNICIPIO() 
        {
            serviceMock.Setup(setup => setup.Delete(id)).ReturnsAsync(true);
            controller = new MunicipioController(serviceMock.Object);
            var resultController = await controller.Delete(id);
            Assert.NotNull(resultController);
            Assert.True(resultController is OkObjectResult);
            var resultContentController = (bool)((OkObjectResult)resultController).Value;
            Assert.True(resultContentController);
            await BadRequest();
        }
        private async Task BadRequest() 
        {
            controller.ModelState.AddModelError("Id","This is a test error.");
            var result = await controller.Delete(id);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
