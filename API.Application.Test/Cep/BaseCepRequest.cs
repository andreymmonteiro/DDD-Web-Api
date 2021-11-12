using application.Controllers;
using Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Application.Test.Cep
{
    public abstract class BaseCepRequest
    {
        protected Mock<ICepService> serviceMock;
        protected CepController controller;
        private Mock<IUrlHelper> url;
        protected const int numeriRandom = 10;

        protected string cep = "95050812";
        protected Guid id = Guid.NewGuid();
        protected string logradouro = Faker.Address.StreetName();
        protected Guid municipioId = Guid.NewGuid();
        protected string numero = Faker.Address.UkPostCode();
        protected DateTime createAt = DateTime.UtcNow;
        protected DateTime updateAt = DateTime.UtcNow.AddHours(2);
        protected BaseCepRequest() 
        {
            serviceMock = new Mock<ICepService>();
            url = new Mock<IUrlHelper>();
            url.Setup(setup => setup.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
        }
        protected void InitializeController() 
        {
            controller = new CepController(serviceMock.Object) { Url = url.Object };
        }
    }
}
