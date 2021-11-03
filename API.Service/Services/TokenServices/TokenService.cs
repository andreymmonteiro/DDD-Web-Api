using Domain.Dtos;
using Domain.Interfaces.Services.Token;
using Domain.Models.Token;
using Domain.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Service.Services.TokenServices
{
    public class TokenService : ITokenService
    {
        private readonly SigningConfigurations signingConfigurations;
        private readonly TokenConfiguration tokenConfiguration;
        private readonly RefreshTokenConfiguration refreshTokenConfiguration;
        private static TokenModel tokenModel { get; set; } = new TokenModel();
        public TokenService(SigningConfigurations signingConfigurations, TokenConfiguration tokenConfiguration, RefreshTokenConfiguration refreshTokenConfiguration)
        {
            this.signingConfigurations = signingConfigurations;
            this.tokenConfiguration = tokenConfiguration;
            this.refreshTokenConfiguration = refreshTokenConfiguration;
            tokenModel.RefreshTokenModel = new RefreshTokenModel();
            tokenModel.RefreshTokenModel.RefreshTokens = new List<(string, string)>();
        }
        public TokenModel GenerateToken(string username, DateTime createDate, DateTime expirionDate)
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsIdentity identity = CreateClaim(username);
            var securityToken = CreateSecurityToken(identity, createDate, expirionDate, handler);
            var refreshToken = GenerateRefreshToken();
            var token = handler.WriteToken(securityToken);
            SaveRefreshToken(username,refreshToken);
            tokenModel.RefreshTokenModel.Expiration = CreateDateExpiration(refreshTokenConfiguration.Seconds, createDate);
            return new TokenModel() { AcessToken = token, RefreshTokenModel = tokenModel.RefreshTokenModel };
        }

        public object SuccessOject(DateTime createDate, DateTime expirionDate, TokenModel token, string username)
        {
            return new
            {
                Authenticated = true,
                Created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                ExpirationToken = expirionDate.ToString("yyyy-MM-dd HH:mm:ss"),
                AcessToken = token.AcessToken,
                RefreshToken = GetRefreshToken(username),
                ExpirationRefreshToken = token.RefreshTokenModel.Expiration.ToString("yyyy-MM-dd HH:mm:ss"),
                Message = "Usuário Logado com sucesso"
            }; 
        }
        public DateTime CreateDateExpiration(int Seconds, DateTime createDate) 
        {
            return createDate + TimeSpan.FromSeconds(Seconds);
        }
        public object ReturnRefreshToken(string token, string refreshToken)
        {
            try
            {
                //Pega o respectivo claim para extrair seu username (Email),com isso não é necessário exigir do backend para envio
                var claimPrincipal = GetPrincipalFromToken(token);
                //Pega o rfresh token antigo
                var oldRefreshToken = GetRefreshToken(claimPrincipal.Identity.Name);
                //deleta o antigo refresh token para posteriormente incluir um novo na lista
                DeleteRefreshToken(claimPrincipal.Identity.Name, oldRefreshToken);

                DateTime createDate = DateTime.Now;
                
                //Se o refresh Token enviado é diferente do atual e retorna como uma informação inválida
                if (oldRefreshToken != refreshToken || createDate >= tokenModel.RefreshTokenModel.Expiration)
                    throw new SecurityTokenException("Invalid Refresh Token! Do login Again");

                DateTime expirationDateToken = CreateDateExpiration(tokenConfiguration.Seconds, createDate);
                tokenModel.RefreshTokenModel.Expiration = CreateDateExpiration(refreshTokenConfiguration.Seconds, createDate);
                //Pega o novo token se tudo ocorreu corretamente
                var newToken = GenerateToken(claimPrincipal.Identity.Name, createDate, expirationDateToken);

                return SuccessOject(createDate, expirationDateToken, newToken, claimPrincipal.Identity.Name);
            }
            catch (SecurityTokenException error)
            {
                throw new SecurityTokenException($"Invalid Refresh Token - {error.Message}");
            }
            return new { };
        }

        private ClaimsIdentity CreateClaim(string username)
        {
            return new ClaimsIdentity(new GenericIdentity(username), new[]
                                                                                            {
                                                                                               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                                                                               new Claim(JwtRegisteredClaimNames.UniqueName, username)
                                                                                            });
        }
        private void SaveRefreshToken(string username, string refreshToken)
        {
            tokenModel.RefreshTokenModel.RefreshTokens.Add(new(username.ToLower(), refreshToken));
        }

        private void DeleteRefreshToken(string username, string refreshToken)
        {
            //List<(string, string)> tokenDelete = tokenModel.RefreshTokens.Where(oldToken => oldToken.Item1.ToLower().Equals(username.ToLower())).ToList();
            tokenModel.RefreshTokenModel.RefreshTokens.RemoveAll(oldToken => oldToken.Item1.ToLower().Equals(username.ToLower()));
        }

        private string GetRefreshToken(string username)
        {
            return tokenModel.RefreshTokenModel.RefreshTokens.LastOrDefault(token => token.Item1.ToLower().Equals(username.ToLower())).Item2;
        }
        private ClaimsPrincipal GetPrincipalFromToken(string token) 
        {
            var tokenValidateionParameters = new TokenValidationParameters()
            {
                ValidAudience = tokenConfiguration.Audience,
                IssuerSigningKey = signingConfigurations.Key,
                ValidIssuer = tokenConfiguration.Issuer,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimResult = tokenHandler.ValidateToken(token,tokenValidateionParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.RsaSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");
            return claimResult;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private SecurityToken CreateSecurityToken(ClaimsIdentity identity, DateTime createDate, DateTime expirionDate, JwtSecurityTokenHandler handler)
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
    }
}
