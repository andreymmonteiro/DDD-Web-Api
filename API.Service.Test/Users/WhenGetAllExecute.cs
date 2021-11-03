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
    public class WhenGetAllExecute : UsersTest
    {
        private IUsersService service;
        private Mock<IUsersService> serviceMock;

        [Fact(DisplayName ="It's possible to execute GetAll")]
        public async Task CAN_EXECUTE_GETALL() 
        {
            serviceMock = new Mock<IUsersService>();
            serviceMock.Setup(setup => setup.GetAll()).ReturnsAsync(ListUserDto);
            service = serviceMock.Object;

            var result = await service.GetAll();
            Assert.True(result.Count() == 10);
            Assert.NotNull(result);

            var listResultEmpty = new List<UserDto>();
            serviceMock = new Mock<IUsersService>();
            serviceMock.Setup(setup => setup.GetAll()).ReturnsAsync(listResultEmpty.AsEnumerable);
            service = serviceMock.Object;

            var resultEmpty = await service.GetAll();
            Assert.Empty(resultEmpty);
            Assert.True(resultEmpty.Count() == 0);
        }
    }
}
