using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace EwalletApi.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {

        // GETs all user non-sensitive data
        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET personal user profile
        [HttpGet("GetProfile/{id}")]
        [Authorize(Roles = "Noob , Elite , Admin")]
        public IActionResult GetProfile(string id)
        {
            return Ok();
        }


        //updates user profile
       [HttpPatch("UpdateUserProfile/{id}")]
       [Authorize(Roles = "Noob , Elite , Admin")]
        public IActionResult UpdateUserProfile(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE personal user account
        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Roles = "Noob, Elite, Admin")]
        public IActionResult DeleteUserAccount(string id)
        {
            return Ok();
        }

        //Upgrades and downgrades a user
        [HttpPatch("ChangeUserRole/{id}")]
        [Authorize(Roles ="Admin")]
        public IActionResult ChangeUserRole(string id)
        {
            return Ok();
        }
    }
}
