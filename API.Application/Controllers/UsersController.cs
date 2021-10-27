using Domain.Dtos.User;
using Domain.Interfaces.Services.Token;
using Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService services;
        private ITokenService tokenService;

        public UsersController(IUsersService services, ITokenService tokenService)
        {
            this.services = services;
            this.tokenService = tokenService;
        }

        [HttpGet]
        
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                //var token = new AuthBaseController(tokenService,Request).CreateToken();
                var resultGet = await services.GetAll();
                return Ok(resultGet);
            }
            //ArgumentException é para tratar error da controller
            catch (ArgumentException error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetWithId")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return Ok(await services.Get(id));
            }
            catch (ArgumentException error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDtoCreate user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await services.Post(user);
                if (result != null)
                    return Created(new Uri(Url.Link("GetWithId", new { id = result.Id })), result);
                return BadRequest(ModelState);
            }
            catch (ArgumentException error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserDtoUpdate user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await services.Put(user);
                if (result != null)
                    return Ok(result);
            }
            catch (ArgumentException error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
            return Ok();

        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                if (await services.Delete(id))
                    return Ok(id);
                return BadRequest(ModelState);

            }
            catch(ArgumentException error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
        }
    }
}
