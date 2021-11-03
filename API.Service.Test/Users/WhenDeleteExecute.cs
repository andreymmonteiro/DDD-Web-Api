using API.Service.Test.Users;
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
    public class WhenDeleteExecute : UsersTest
    {
        private IUsersService service;
        private Mock<IUsersService> serviceMock;

        [Fact(DisplayName ="It's possible to delete.")]
        public async Task CAN_DELETE()
        {
            serviceMock = new Mock<IUsersService>();
            serviceMock.Setup(setup => setup.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            service = serviceMock.Object;

            var isDeleted = await service.Delete(IdUser);
            Assert.True(isDeleted);

            serviceMock = new Mock<IUsersService>();

        }

    }
}
