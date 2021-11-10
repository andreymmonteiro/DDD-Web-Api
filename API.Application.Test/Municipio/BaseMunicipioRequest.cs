using application.Controllers;
using Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Application.Test.Municipio
{
    public abstract class BaseMunicipioRequest
    {
        private IUrlHelper url;
        protected MunicipioController controller;
        protected Mock<IMunicipioService> serviceMock;
        protected Guid id = Guid.NewGuid();
        protected int codigoIbge = Faker.RandomNumber.Next(1000000, 9999999);
        protected Guid ufId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"); 
        protected string name = Faker.Address.StreetName();
        protected const int numericRandom = 10;
        protected BaseMunicipioRequest() 
        {
            serviceMock = new Mock<IMunicipioService>();
            Mock<IUrlHelper> urlMock = new Mock<IUrlHelper>();
            urlMock.Setup(setup => setup.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            url = urlMock.Object;
        }
        
        protected void InitializeController() 
        {
            controller = new MunicipioController(serviceMock.Object) { Url= url};
        }
    }
}
