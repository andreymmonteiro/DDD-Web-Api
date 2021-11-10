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
    public class RequiredGetAll : BaseUserRequest
    {
        private UsersController usersController;

        [Fact(DisplayName ="Can Get All.")]
        public async Task CAN_GETAL() 
        {
            Mock<IUsersService> serviceMock = new Mock<IUsersService>();
            List<UserDto> users = new List<UserDto>();
            for (int i = 0; i < 10; i++)
                users.Add(new UserDto()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                });
            serviceMock.Setup(setup => setup.GetAll()).ReturnsAsync(users);
            usersController = new UsersController(serviceMock.Object) { Url = url};
            var result = await usersController.GetAll();
            var resultObject = ((List<UserDto>)((OkObjectResult)result).Value);
            Assert.True(result is OkObjectResult);
            Assert.False(resultObject.Count() == 0);
            for (int i = 0; i < 10; i++) 
            {
                Assert.Equal(users[i].Id, resultObject[i].Id);
                Assert.Equal(users[i].Name, resultObject[i].Name);
                Assert.Equal(users[i].Email, resultObject[i].Email);
            }
            await BAD_REQUEST(serviceMock);
        }
        private async Task BAD_REQUEST(Mock<IUsersService> serviceMock) 
        {
            UsersController controller = new UsersController(serviceMock.Object) { Url = url};
            controller.ModelState.AddModelError("Name", "This is an invalid Name");
            var result = await controller.GetAll();
            Assert.True(result is BadRequestObjectResult);
            Assert.False(controller.ModelState.IsValid);
        }
    }
}
