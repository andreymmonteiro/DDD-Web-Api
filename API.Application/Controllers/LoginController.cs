using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto user, [FromServices] ILoginService service) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (user == null)
                return BadRequest();
            try
            {
                var result = await service.FindByLogin(user);
                if (result == null)
                    return NotFound();
                return Ok(result);

            }catch(ArgumentException error) 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
        }
        
    }
}
