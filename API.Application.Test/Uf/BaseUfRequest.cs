using application.Controllers;
using Domain.Interfaces.Services.Uf;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Application.Test.Uf
{
    public abstract class BaseUfRequest
    {
        protected Mock<IUfService> serviceMock;
        protected UfController controller;
        protected const string name = "Distrito Federal";
        protected readonly DateTime createAt = DateTime.UtcNow;
        protected const string sigla = "DF";
        protected readonly Guid id = new Guid("bd08208b-bfca-47a4-9cd0-37e4e1fa5006");

        protected BaseUfRequest()
        {
            serviceMock = new Mock<IUfService>();
        }
        
        protected void InitializeController() 
        {
            controller = new UfController(serviceMock.Object);
        }
    }
}
