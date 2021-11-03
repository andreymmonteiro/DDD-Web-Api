using Domain.Dtos;
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
    public class WhenLoginUser : UsersTest
    {
        private ILoginService service;
        private Mock<ILoginService> serviceMock;

        [Fact(DisplayName ="Can Login")]
        public async Task CAN_LOGIN() 
        {
            var email = Faker.Internet.Email();
            var loginDto = new LoginDto()
            {
                Email = email
            };

            var objectReturn = new
            {
                Authenticated = true,
                Created = DateTime.UtcNow,
                ExpirationToken = DateTime.UtcNow.AddHours(2),
                AcessToken = Guid.NewGuid(),
                RefreshToken = Guid.NewGuid(),
                ExpirationRefreshToken = DateTime.Now.AddHours(12),
                Message = "Usuário Logado com sucesso",
                Email = email
            };
            serviceMock = new Mock<ILoginService>();
            serviceMock.Setup(setup => setup.FindByLogin(loginDto)).ReturnsAsync(objectReturn);

            service = serviceMock.Object;

            var result = await service.FindByLogin(loginDto);

            Assert.NotNull(result);
            Assert.Equal(objectReturn, result);
        }
    }
}
