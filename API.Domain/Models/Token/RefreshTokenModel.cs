using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Token
{
    public class RefreshTokenModel
    {
        public DateTime Expiration { get; set;  }
        public List<(string, string)> RefreshTokens { get; set; }
    }
}
