using API.Service.Test.Users;
using Domain.Interfaces.Services.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Service.Test
{
    public class WhenUpdateExecute : UsersTest
    {
        private IUsersService service;
        private Mock<IUsersService> serviceMock;

        [Fact(DisplayName = "It's possible to update.")]
        public async Task CAN_UPDATE() 
        {

            serviceMock = new Mock<IUsersService>();
            serviceMock.Setup(setup => setup.Put(userDtoUpdate)).ReturnsAsync(userDtoUpdateResult);
            service = serviceMock.Object;

            var resultUpdate = await service.Put(userDtoUpdate);
            Assert.NotNull(resultUpdate);
            Assert.Equal(AlterNameUser, resultUpdate.Name);
            Assert.Equal(AlterEmailUser, resultUpdate.Email);
            
        }
    }
}
