﻿using EwalletApi.Services.AuthService.Interfaces;
using EwalletApi.UI.DTO;
using EwalletApi.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class AuthorizationController : ControllerBase
    {
        //private readonly IJwtService jwt;
        private readonly ILogin _login;

        public AuthorizationController(ILogin login)
        {
            _login = login;
          //  jwt = _jwt;
        }

       
        [HttpPost("Login")]
        public IActionResult Login([FromForm]LoginDTO credentials )
        {
            var token = _login.LogIn(credentials);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
        
      
        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterDTO details)
        {

            return Ok();
        }
      
    }
}
