using Domain.Dtos.Cep;
using Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class CepController : ControllerBase
    {
        private ICepService service;

        public CepController(ICepService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("id/{Id}", Name = "GetCepById")]
        public async Task<ActionResult> Get(Guid Id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await service.Get(Id);
                if (result == null)
                    return BadRequest(ModelState);

                return Ok(result);
            }
            catch (ArgumentException error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
        }
        [HttpGet]
        [Route("{Cep}", Name = "GetCepByCep")]
        public async Task<ActionResult> Get(string Cep)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await service.Get(Cep);
                if (result == null)
                    return BadRequest(ModelState);
                return Ok(result);

            }
            catch (ArgumentException error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await service.GetAll();
                if (result == null)
                    return BadRequest(ModelState);
                return Ok(result);

            }
            catch (ArgumentException error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CepDtoCreate cepDtoCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await service.Post(cepDtoCreate);
                if (result == null)
                    return BadRequest(ModelState);
                return Created(new Uri(Url.Link("GetCepById", new { Id = result.Id })), result);
            }
            catch (ArgumentException error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CepDtoUpdate cepDtoUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await service.Put(cepDtoUpdate);
                if (result == null)
                    return BadRequest(ModelState);
                return Ok(result);
            }
            catch (ArgumentException error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
        }
        [HttpDelete]
        [Route("{Id}")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await service.Delete(Id);
                if (!result)
                    return BadRequest(ModelState);
                return Ok(result);
            }
            catch (ArgumentException error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
        }
    }
}
