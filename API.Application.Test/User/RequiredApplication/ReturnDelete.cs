using application.Controllers;
using Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Application.Test.User.RequiredApplication
{
    public class ReturnDelete : BaseRequired
    {
        private UsersController usersController;
        [Fact(DisplayName ="Can Delete this.")]
        public async Task CAN_DELETE() 
        {
            Id = Guid.NewGuid();
            Mock<IUsersService> serviceMock = new Mock<IUsersService>();
            serviceMock.Setup(setup => setup.Delete(Id)).ReturnsAsync(true);
            usersController = new UsersController(serviceMock.Object) { Url = url};
            var result = await usersController.Delete(Id);
            Assert.True(result is OkObjectResult);
            var resultObject = ((Guid)((OkObjectResult)result).Value);
            Assert.Equal(Id, resultObject);
        }
        private async Task BAD_REQUEST(Mock<IUsersService> serviceMock) 
        {
            UsersController controller = new UsersController(serviceMock.Object) { Url = url };
            controller.ModelState.AddModelError("Id", "Id incorreto");
            var result = await controller.Delete(Id);
            
            Assert.True(result is BadRequestObjectResult);
            Assert.False(controller.ModelState.IsValid);
        }
    }
}
