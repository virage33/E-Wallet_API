using Ewallet.Core.Interfaces;
using Ewallet.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Login([FromForm]LoginDTO credentials )
        {
            var token = await authService.Login(credentials);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }


       
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO details)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please enter valid data");
            
            if (details.ConfirmPassword != details.Password)
                return BadRequest("Password mismatch!");
            
            if (details.Role.ToLower() != "noob" && details.Role.ToLower() != "elite")
                return BadRequest("User must be Noob or Elite");

            var existing = await authService.Register(details);

            if (existing == "exists")
                return Conflict("User Exists!");

            return Ok("successful");
        }

        [HttpPost("Forgot Password")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordDTO email)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please enter valid data");
            var response = await authService.ForgotPassword(email);
            if (response == "0")
                return BadRequest("User does not Exist!");

            return Ok(response);
        }

        [Authorize]
        [HttpPost("Logout")]
        public  IActionResult Logout()
        {
            return Ok();
        }

    }
}
