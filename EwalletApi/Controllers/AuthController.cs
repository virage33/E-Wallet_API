using Ewallet.Core.DTO;
using Ewallet.Core.Interfaces;
using EwalletApi.Services.AuthService.Interfaces;

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
 
    public class AuthController : ControllerBase
    {
        
        private readonly ILogin _login;
        private readonly IAuthService authService; 


        public AuthController(ILogin login, IAuthService auth)
        {
            _login = login;
            authService = auth;
        
        }

       
        [HttpPost("Login")]
        public IActionResult Login([FromForm]LoginDTO credentials )
        {
            var token = _login.LogIn(credentials);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

       // [Authorize(Roles = "Noob , Elite")]
        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterDTO details)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (details.ConfirmPassword != details.Password)
                return BadRequest();

            // UserDTO existing = await getuserbyemal;
            // if (existing!=null)
            //  return conflict();

            

            return Ok(authService.Register(details));
        }

        [HttpPost("Forgot Password")]
        public IActionResult ForgotPassword([FromBody]ChangePasswordDTO details)
        {
            return Ok();
        }
        [Authorize]
        [HttpPost]
        public  IActionResult Logout()
        {
            return Ok();
        }

    }
}
