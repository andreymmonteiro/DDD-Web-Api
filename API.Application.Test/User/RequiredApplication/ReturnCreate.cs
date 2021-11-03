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
    public class ReturnCreate : BaseRequired
    {
        private UsersController usersController;

        [Fact(DisplayName ="Can return create from controller")]
        public async Task CAN_CREATE()
        {
            var serviceMock = new Mock<IUsersService>();
            serviceMock.Setup(setup => setup.Post(It.IsAny<UserDtoCreate>())).ReturnsAsync(new UserDtoCreateResult()
            {
                Name = name,
                Email = email,
                CreateAt = DateTime.UtcNow,
                Id = Guid.NewGuid()
            });
            usersController = new UsersController(serviceMock.Object);
            usersController.Url = url;
            var userDtoCreate = new UserDtoCreate() { Name = name, Email =email };
            var result = await usersController.Post(userDtoCreate);

            Assert.NotNull(result);
            Assert.True(result is CreatedResult);

            var userDtoCreateResult = ((UserDtoCreateResult)((CreatedResult)result).Value);
            Assert.NotNull(userDtoCreateResult);
            Assert.Equal(userDtoCreateResult.Name, userDtoCreate.Name);
            Assert.Equal(userDtoCreateResult.Email, userDtoCreate.Email);
        }
    }
}
