using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Token
{
    public class TokenModel
    {
        public string AcessToken { get; set; }
        public string RefreshToken { get; set; }
        public List<(string, string)> RefreshTokens { get; set; } = new List<(string, string)>();

    }
}
