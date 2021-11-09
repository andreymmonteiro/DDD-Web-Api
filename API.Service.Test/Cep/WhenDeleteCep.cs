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
    public class WhenDeleteCep : BaseCep
    {
        [Fact(DisplayName ="Can Delete Cep.")]
        public async Task CAN_DELETE_CEP()
        {
            serviceMock = new Mock<ICepService>();
            serviceMock.Setup(setup => setup.Delete(id)).ReturnsAsync(true);
            service = serviceMock.Object;
            var resultDelete = await service.Delete(id);
            Assert.True(resultDelete);
        }
    }    
}
