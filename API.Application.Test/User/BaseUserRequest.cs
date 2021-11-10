using application.Controllers;
using Domain.Dtos.User;
using Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Application.Test.User
{
    public abstract class BaseUserRequest
    {
        protected IUrlHelper url { get; set; }
        protected Guid Id { get; set; }
        protected string name = Faker.Name.FullName();
        protected string email = Faker.Internet.Email();
        public BaseUserRequest()
        {
            Mock<IUrlHelper> urlHelper = new Mock<IUrlHelper>();
            urlHelper.Setup(setup => setup.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            url = urlHelper.Object;
        }
    }
}
