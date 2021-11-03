using application.Controllers;
using Domain.Dtos.User;
using Domain.Interfaces.Services.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Application.Test.User.RequiredApplication
{
    public class ReturnUpdate : BaseRequired
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
            
        }
    }
}
