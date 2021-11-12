using AutoMapper;
using Domain.Dtos.Municipio;
using Domain.Interfaces.Services.Municipio;
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
    public class MunicipioController : ControllerBase
    {
        private readonly IMunicipioService service;

        public MunicipioController(IMunicipioService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            if (!ModelState.IsValid)
                BadRequest();
            return Ok(await service.GetAll());
        }
        [HttpGet]
        [Route("{id}",Name ="GetMunicipioById")]
        public async Task<ActionResult> Get(Guid Id) 
        {
            try 
            {
                if (!ModelState.IsValid)
                    BadRequest();
                var MunicipioDto = await service.Get(Id);
                if (MunicipioDto == null)
                    BadRequest("This Municipio doesn't exist.");

                return Ok(MunicipioDto);
            }catch (ArgumentException ex) 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MunicipioDtoCreate municipioCreateDto) 
        {
            try 
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var municipioDtoCreateResult = await service.Post(municipioCreateDto);
                if (municipioDtoCreateResult == null)
                    return BadRequest();
                return Created(new Uri(Url.Link("GetMunicipioById", new { Id = municipioDtoCreateResult.Id })), municipioDtoCreateResult);
            }
            catch (ArgumentException ex) 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] MunicipioDtoUpdate municipioDtoUpdate) 
        {
            try 
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await service.Put(municipioDtoUpdate);
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
        [Route("{id}")]
        public async Task<ActionResult> Delete(Guid id) 
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await service.Delete(id);
                if(!result)
                    return BadRequest(ModelState);
                return Ok(result);
            }
            catch (ArgumentException error) 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,error.Message);
            }
        }
    }
}
