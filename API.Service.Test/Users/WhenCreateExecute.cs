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
    public class WhenCreateExecute : UsersTest
    {
        private IUsersService service;
        private Mock<IUsersService> serviceMock;

        [Fact(DisplayName ="It's Possible to execute Create.")]
        public async Task CAN_CREATE() 
        {
            serviceMock = new Mock<IUsersService>();
            serviceMock.Setup(setup => setup.Post(userCreateDto)).ReturnsAsync(userDtoCreateResult);
            service = serviceMock.Object;

            var resultCreate = await service.Post(userCreateDto);
            Assert.NotNull(resultCreate);
            Assert.Equal(NameUser, resultCreate.Name);
            Assert.Equal(EmailUser, resultCreate.Email);
        }
    }
}
