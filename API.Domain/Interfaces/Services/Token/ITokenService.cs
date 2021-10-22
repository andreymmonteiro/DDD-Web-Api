using Domain.Dtos;
using Domain.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Token
{
    public interface ITokenService
    {
        string GenerateRefreshToken();
        string GenerateToken(LoginDto user, TokenConfiguration tokenConfiguration, DateTime createDate, DateTime expirionDate);
        SecurityToken CreateSecurityToken(ClaimsIdentity identity, TokenConfiguration tokenConfiguration, DateTime createDate, DateTime expirionDate, JwtSecurityTokenHandler handler);
        object SuccessOject(DateTime createDate, DateTime expirionDate, string token, LoginDto user);
        ClaimsIdentity CreateClaim(LoginDto user);
    }
}
