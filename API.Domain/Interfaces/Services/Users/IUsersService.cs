using Domain.Dtos.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Users
{
    public interface IUsersService
    {
        Task<UserDto> Get(Guid id);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDtoCreateResult> Post(UserDtoCreate entity);
        Task<UserDtoUpdateResult> Put(UserDtoUpdate entity);
        Task<bool> Delete(Guid id);
    }
}
