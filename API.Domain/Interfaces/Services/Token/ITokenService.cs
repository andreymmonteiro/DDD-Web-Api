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
        string GenerateToken(string username, DateTime createDate, DateTime expirionDate);
        object SuccessOject(DateTime createDate, DateTime expirionDate, string token, string username);
        object ReturnRefreshToken(string token, string refreshToken);
    }
}
