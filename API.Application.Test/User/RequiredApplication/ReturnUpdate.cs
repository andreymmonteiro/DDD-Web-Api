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
    public class ReturnUpdate : BaseUserRequest
    {
        private UsersController usersController;

        [Fact(DisplayName ="Can update test.")]
        public async Task CAN_UPDATE() 
        {
            Id = Guid.NewGuid();
            var userDtoUpdate = new UserDtoUpdate() 
            {
                Id = Id,
                Name = name,
                Email = email
            };
            Mock<IUsersService> serviceMock = new Mock<IUsersService>();
            serviceMock.Setup(setup => setup.Put(userDtoUpdate)).ReturnsAsync(new UserDtoUpdateResult() 
            {
                Id = Id,
                Name = name,
                Email = email,
                UpdateAt = DateTime.UtcNow
            });
            usersController = new UsersController(serviceMock.Object) { Url = url};
            var result = await usersController.Put(userDtoUpdate);
            Assert.True(result is OkObjectResult);

            var dtoUpdateResult = (UserDtoUpdateResult)((OkObjectResult)result).Value;
            Assert.NotNull(dtoUpdateResult);
            Assert.Equal(dtoUpdateResult.Name, name);
            Assert.Equal(dtoUpdateResult.Email, email);
            await BAD_REQUEST(serviceMock, userDtoUpdate);
        }
        public async Task BAD_REQUEST(Mock<IUsersService> serviceMock, UserDtoUpdate user) 
        {
            UsersController controller = new UsersController(serviceMock.Object) { Url = url };
            controller.ModelState.AddModelError("Email", "Email is not valid");
            var result  = await controller.Put(user);
            Assert.False(controller.ModelState.IsValid);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
