using Domain.Dtos.Uf;
using Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class UfController : ControllerBase
    {
        private readonly IUfService service;

        public UfController(IUfService service)
        {
            this.service = service;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> Get([FromQuery]Guid Id) 
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if(Id == Guid.Empty)
                return BadRequest("This is not a valid Guid information.");
            return Ok(await service.Get(Id));

        }
        [HttpGet]
        public async Task<ActionResult> Get() 
        {
            if(!ModelState.IsValid)
                return BadRequest();
            return Ok(await service.GetAll());
        }
    }
}
