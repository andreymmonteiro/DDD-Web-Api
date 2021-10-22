using Domain.Dtos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Domain.Security.TokenHandlerConfiguration
{
    public class Token
    {
        public static string CreateToken(LoginDto user, DateTime createDate, DateTime expirionDate, TokenConfiguration tokenConfiguration, SigningConfigurations signingConfigurations)
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsIdentity identity = CreateClaim(user);
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
        public static object SuccessOject(DateTime createDate, DateTime expirionDate, string token, LoginDto user)
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
        private static ClaimsIdentity CreateClaim(LoginDto user)
        {
            return new ClaimsIdentity(new GenericIdentity(user.Email), new[]
                                                                                            {
                                                                                               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                                                                               new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                                                                                            });
        }
    }
}
