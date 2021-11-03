using API.Service.Test.Users;
using Domain.Dtos.User;
using Domain.Interfaces.Services.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Service.Test.Users
{
    public class WhenGetExecute : UsersTest 
    {
        private IUsersService service;
        private Mock<IUsersService> serviceMock;

        [Fact(DisplayName ="It's possible to execute Get Method")]
        public async Task CAN_EXECUTE_GET_METHOD() 
        {
            serviceMock = new Mock<IUsersService>();
            serviceMock.Setup(setup => setup.Get(IdUser)).ReturnsAsync(userDto);
            service = serviceMock.Object;
            var resultGet = await service.Get(IdUser);
            Assert.NotNull(resultGet);
            Assert.True(resultGet.Id == IdUser);
            Assert.Equal(resultGet.Name, NameUser);
            Assert.Equal(resultGet.Email, EmailUser);

            serviceMock.Setup(setup => setup.Get(It.IsAny<Guid>())).Returns(Task.FromResult((UserDto) null));
            service = serviceMock.Object;
            var resultNull = await service.Get(IdUser);
            Assert.Null(resultNull);
        }
        
    }
}
