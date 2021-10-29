using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IUserRepository 
    {
        Task<UsersEntity> FindByLogin(string email);
    }
}
