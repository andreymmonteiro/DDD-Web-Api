using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Users
{
    public interface ILoginService
    {
        Task<object> FindByLogin(LoginDto user);
    }
}
