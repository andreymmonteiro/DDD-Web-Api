using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Integration.Test
{
    public class LoginTest :BaseIntegration
    {
        [Fact]
        public async Task Test() 
        {
            await AddToken();
        }
    }
}
