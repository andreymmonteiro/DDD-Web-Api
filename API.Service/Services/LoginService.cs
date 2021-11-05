using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces.Services.Token;
using Domain.Interfaces.Services.Users;
using Domain.Repository;
using Domain.Security;
using System;
using System.Threading.Tasks;

namespace Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository userRepository;
        private TokenConfiguration tokenConfiguration;
        private ITokenService token;

        public LoginService(IUserRepository repository, SigningConfigurations signingConfigurations, TokenConfiguration tokenConfiguration, ITokenService token)
        {
            userRepository = repository;
            this.tokenConfiguration = tokenConfiguration;
            this.token = token;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            UsersEntity baseUser = null;
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
                baseUser = await userRepository.FindByLogin(user.Email);
            if (baseUser == null)
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            
            DateTime createDate = DateTime.Now;
            
            DateTime expirionDate =  token.CreateDateExpiration(tokenConfiguration.Seconds,createDate);
            

            //var token = Token.GenerateToken(user, createDate, expirionDate, tokenConfiguration, signingConfigurations);
            var tokenResult = token.GenerateToken(user.Email,createDate, expirionDate);
            
            return  token.SuccessOject(createDate, expirionDate, tokenResult, user.Email);
        }
    }
}
