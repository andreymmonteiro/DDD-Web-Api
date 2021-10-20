using Domain.Dtos;
using Domain.Interfaces.Services.Users;
using Domain.Repository;
using System.Threading.Tasks;

namespace Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository repository;

        public LoginService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            if(user != null && !string.IsNullOrWhiteSpace(user.Email))
                return await repository.FindByLogin(user.Email);
            return null;
        }
    }
}
