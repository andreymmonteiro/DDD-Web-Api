using application.Controllers;
using Domain.Dtos.User;
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
    public class RequiredGet : BaseRequired
    {
        UsersController usersController;

        [Fact(DisplayName = "Can Get.")]
        public async Task CAN_GET()
        {
            Id = Guid.NewGuid();
            Mock<IUsersService> serviceMock = new Mock<IUsersService>();
            serviceMock.Setup(setup => setup.Get(Id)).ReturnsAsync(new UserDto()
            {
                Id = Id,
                Name = name,
                Email = email
            });
            usersController = new UsersController(serviceMock.Object) { Url = url };
            var result = await usersController.Get(Id);
            Assert.True(result is OkObjectResult);
            var resultObjetc = ((UserDto)((OkObjectResult)result).Value);
            Assert.Equal(Id, resultObjetc.Id);
            Assert.Equal(name, resultObjetc.Name);
            Assert.Equal(email, resultObjetc.Email);
            await BAD_REQUEST(serviceMock);
        }
        private async Task BAD_REQUEST(Mock<IUsersService> serviceMock) 
        {
            UsersController controller = new UsersController(serviceMock.Object) { Url = url };
            controller.ModelState.AddModelError("Name", "Name is incorrect.");
            var result = await controller.Get(Id);
            Assert.False(controller.ModelState.IsValid);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
