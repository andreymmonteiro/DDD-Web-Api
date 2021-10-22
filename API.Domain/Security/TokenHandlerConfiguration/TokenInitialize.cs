using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Security.TokenHandlerConfiguration
{
    public class TokenInitialize
    {
        public JwtSecurityTokenHandler Handler { get; set; }
        public SecurityToken SecurityToken { get; set; }

        public TokenInitialize(JwtSecurityTokenHandler handler, SecurityToken securityToken)
        {
            Handler = handler;
            SecurityToken = securityToken;
            //handler.WriteToken(securityToken);
        }
        
    }
}
