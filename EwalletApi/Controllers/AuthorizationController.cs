using EwalletApi.Services.AuthService.Interfaces;
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
        
        private readonly ILogin _login;

        public AuthorizationController(ILogin login)
        {
            _login = login;
        
        }

       
        [HttpPost("Login")]
        public IActionResult Login([FromForm]LoginDTO credentials )
        {
            var token = _login.LogIn(credentials);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        [Authorize(Roles = "Noob , Elite")]
        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterDTO details)
        {

            return Ok();
        }

        [HttpPost("Forgot Password")]
        public IActionResult ForgotPassword([FromBody]ChangePasswordDTO details)
        {
            return Ok();
        }
      
    }
}
