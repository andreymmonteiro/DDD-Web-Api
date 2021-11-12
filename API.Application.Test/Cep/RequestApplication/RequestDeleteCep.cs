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
    public class RequestDeleteCep : BaseCepRequest
    {
        [Fact(DisplayName ="Can Delete Test Application.")]
        public async Task CAN_DELETE_CEP() 
        {
            serviceMock.Setup(setup => setup.Delete(id)).ReturnsAsync(true);
            InitializeController();
            var resultController = await controller.Delete(id);
            Assert.True(resultController is OkObjectResult);
            var resultContentController = (bool)((OkObjectResult)resultController).Value;
            Assert.True(resultContentController);
            await BadRequest();
        }
        private async Task BadRequest() 
        {
            controller.ModelState.AddModelError("Id", "Error Delete Test Application.");
            var resultController = await controller.Delete(id);
            Assert.True(resultController is BadRequestObjectResult);
        }
    }
}
