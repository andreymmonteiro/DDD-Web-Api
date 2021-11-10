using Domain.Dtos.Municipio;
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
    public class RequestPostMunicipio : BaseMunicipioRequest
    {
        [Fact(DisplayName ="Can Post Municipio Controller")]
        public async Task CAN_POST_MUNICIPIO() 
        {
            MunicipioDtoCreate municipioDtoCreate = new MunicipioDtoCreate() 
            {
                Name =name,
                UfId = ufId,
                CodigoIbge = codigoIbge
            };
            MunicipioDtoCreateResult municipioDtoCreateResult = new MunicipioDtoCreateResult() 
            {
                Id = id,
                Name = name,
                UfId = ufId,
                CodigoIbge = codigoIbge,
                CreateAt = DateTime.UtcNow
            };
            serviceMock.Setup(setup => setup.Post(municipioDtoCreate)).ReturnsAsync(municipioDtoCreateResult);
            InitializeController();
            var resultPost = await controller.Post(municipioDtoCreate);
            Assert.True(controller.ModelState.IsValid);
            Assert.True(resultPost is CreatedResult);
            var resultMunicipioDtoCreateResult = (MunicipioDtoCreateResult)((CreatedResult)resultPost).Value;
            Assert.False(resultMunicipioDtoCreateResult.Id == Guid.Empty);
            Assert.Equal(municipioDtoCreate.Name, municipioDtoCreateResult.Name);
            Assert.Equal(municipioDtoCreate.UfId, municipioDtoCreateResult.UfId);
            Assert.Equal(municipioDtoCreate.CodigoIbge, municipioDtoCreateResult.CodigoIbge);
            await BadRequest(municipioDtoCreate);
        }
        private async Task BadRequest(MunicipioDtoCreate municipioDtoCreate)
        {
            controller.ModelState.AddModelError("Id", "Invalid Model State Did With Mock.");
            var resultPost = await controller.Post(municipioDtoCreate);
            Assert.True(resultPost is BadRequestObjectResult);
        }
    }
}
