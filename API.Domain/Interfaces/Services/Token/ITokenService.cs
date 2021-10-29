using Domain.Dtos;
using Domain.Models.Token;
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
        TokenModel GenerateToken(string username, DateTime createDate, DateTime expirionDate);
        object SuccessOject(DateTime createDate, DateTime expirionDate, TokenModel token, string username);
        object ReturnRefreshToken(string token, string refreshToken);
        DateTime CreateDateExpiration(int Seconds, DateTime createTime);

    }
}
