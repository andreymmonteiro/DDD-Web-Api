﻿using Domain.Entities;
using Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService services;

        public UsersController(IUsersService services)
        {
            this.services = services;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await services.GetAll());
            }
            //ArgumentException é para tratar error da controller
            catch (ArgumentException error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }


        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return Ok(await services.Get(id));
            }
            catch(ArgumentException error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var restult = await services.Post(user);
            }
            catch (ArgumentException error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
            return Ok();
        }
    }
}
