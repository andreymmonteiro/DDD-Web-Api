using Domain.Interfaces.Services.Token;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthRefreshBaseController : Controller
    {
        private ITokenService tokenService;

        public AuthRefreshBaseController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }
        [HttpPost]
        public ActionResult Post([FromQuery]string refreshToken)
        {
            try
            {
                var tokenRequest = ((string)Request.Headers.FirstOrDefault(header => header.Key.Equals("Authorization")).Value).Remove(0, 7);
                //var refreshToken = Request.Headers.FirstOrDefault(header => header.Key.Equals("RefreshToken")).Value;
                var result = tokenService.ReturnRefreshToken(tokenRequest, refreshToken);
                return Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }   
        }
    }
}
