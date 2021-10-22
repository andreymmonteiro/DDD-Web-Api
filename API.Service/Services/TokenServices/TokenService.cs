using Domain.Dtos;
using Domain.Dtos.User;
using Domain.Interfaces.Services.Token;
using Domain.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace Service.Services.TokenServices
{
    public class TokenService : ITokenService
    {
        private SigningConfigurations signingConfigurations;

        public TokenService(SigningConfigurations signingConfigurations)
        {
            this.signingConfigurations = signingConfigurations;
            
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string GenerateToken(LoginDto user, TokenConfiguration tokenConfiguration, DateTime createDate, DateTime expirionDate)
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsIdentity identity = CreateClaim(user);
            var securityToken = CreateSecurityToken( identity, tokenConfiguration, createDate, expirionDate, handler);
            return handler.WriteToken(securityToken);
        }
        public SecurityToken CreateSecurityToken(ClaimsIdentity identity, TokenConfiguration tokenConfiguration, DateTime createDate, DateTime expirionDate, JwtSecurityTokenHandler handler)
        {
            return handler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = tokenConfiguration.Issuer,
                Audience = tokenConfiguration.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirionDate
            });
        }
        public object SuccessOject(DateTime createDate, DateTime expirionDate, string token, LoginDto user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirionDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                message = "Usuário Logado com sucesso"
            };
        }
        public ClaimsIdentity CreateClaim(LoginDto user)
        {
            return new ClaimsIdentity(new GenericIdentity(user.Email), new[]
                                                                                            {
                                                                                               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                                                                               new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                                                                                            });
        }

    }
}
