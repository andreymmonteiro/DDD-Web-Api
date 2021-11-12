using application.Controllers;
using Domain.Dtos.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Application.Test.Municipio.RequestApplication
{
    public class RequestPutMunicipio : BaseMunicipioRequest
    {
        [Fact(DisplayName ="Can update Municipio test application.")]
        public async Task CAN_UPDATE_MUNICIPIO() 
        {
            MunicipioDtoUpdate municipioDtoUpdate = new MunicipioDtoUpdate() 
            {
                Id= id,
                Name = name,
                CodigoIbge = codigoIbge,
                UfId = ufId
            };
            MunicipioDtoUpdateResult municipioDtoupdateresult = new MunicipioDtoUpdateResult()
            {
                Id = id,
                Name = name,
                CodigoIbge = codigoIbge,
                UfId = ufId,
                UpdateAt = DateTime.UtcNow
            };
            serviceMock.Setup(setup => setup.Put(municipioDtoUpdate)).ReturnsAsync(municipioDtoupdateresult);
            controller = new MunicipioController(serviceMock.Object);
            var resultController = await controller.Put(municipioDtoUpdate);
            Assert.NotNull(resultController);
            Assert.True(resultController is OkObjectResult);
            var resultContentController = (MunicipioDtoUpdateResult)((OkObjectResult)resultController).Value;
            Assert.NotNull(resultContentController);
            Assert.False(resultContentController.Id == Guid.Empty);
            Assert.Equal(municipioDtoUpdate.Id, resultContentController.Id);
            Assert.Equal(municipioDtoUpdate.Name, resultContentController.Name);
            Assert.Equal(municipioDtoUpdate.CodigoIbge, resultContentController.CodigoIbge);
            Assert.Equal(municipioDtoUpdate.UfId, resultContentController.UfId);
            await BadRequest(municipioDtoUpdate);
        }
        private async Task BadRequest(MunicipioDtoUpdate municipioDtoUpdate) 
        {
            controller.ModelState.AddModelError("Id","This is a test error");
            var result = await controller.Put(municipioDtoUpdate);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
