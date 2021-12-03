using Ewallet.Core.DTO;
using Ewallet.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EwalletApi.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class AuthController : ControllerBase
    {
        
    
        private readonly IAuthService authService; 


        public AuthController(IAuthService auth)
        {
           
            authService = auth;
        
        }

       
        [HttpPost("Login")]
        public IActionResult Login([FromForm]LoginDTO credentials )
        {
            var token = authService.Login(credentials);
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
        public IActionResult ForgotPassword([FromBody]string email)
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
