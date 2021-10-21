using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces.Services.Users;
using Domain.Repository;
using Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository repository;
        private SigningConfigurations signingConfigurations;
        private TokenConfiguration tokenConfiguration;

        public LoginService(IUserRepository repository, SigningConfigurations signingConfigurations, TokenConfiguration tokenConfiguration)
        {
            this.repository = repository;
            this.signingConfigurations = signingConfigurations;
            this.tokenConfiguration = tokenConfiguration;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            UserEntity baseUser = null;
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
                baseUser = await repository.FindByLogin(user.Email);
            if (baseUser == null)
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.Email), new[]
                                                                                            {
                                                                                               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                                                                               new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                                                                                            });
            DateTime createDate = DateTime.Now;
            DateTime expirionDate = createDate + TimeSpan.FromSeconds(tokenConfiguration.Seconds);
            var handler = new JwtSecurityTokenHandler();
            var token = CreateToken(identity, createDate, expirionDate, handler);

            return SuccessOject(createDate, expirionDate, token, user);
        }
        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirionDate, JwtSecurityTokenHandler handler) 
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor() 
            {
                Issuer = tokenConfiguration.Issuer,
                Audience = tokenConfiguration.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirionDate
            });
            return handler.WriteToken(securityToken);
        }
        private object SuccessOject(DateTime createDate, DateTime expirionDate, string token, LoginDto user) 
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirionDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken =token,
                message = "Usuário Logado com sucesso"
            };
        }
    }
}
