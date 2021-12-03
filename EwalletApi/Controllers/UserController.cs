using Ewallet.Core.DTO;
using Ewallet.Core.Interfaces;
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
        private IUserService UserService { get; set; }
        public UserController(IUserService userService)
        {
            UserService = userService;
        }


        // GETs all user non-sensitive data
        [HttpGet("GetAllUsers")]
       // [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<UserDTO>> Get()
        {
            var result = await UserService.GetAllUsers();
            return result;
        }

        // GET personal user profile
        [HttpGet("GetProfile/{id}")]
        [Authorize(Roles = "Noob , Elite , Admin")]
        public async Task<IActionResult> GetProfile(string id)
        {
            return Ok();
        }

        [HttpGet("GetUsersByName")]
        // [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<UserDTO>> GetUsersByName(string name)
        {
            var result = await UserService.GetUsersByName(name);           
            return result;
        }

        //updates user profile
        [HttpPatch("UpdateUserProfile/{id}")]
       [Authorize(Roles = "Noob , Elite , Admin")]
        public async Task<IActionResult> UpdateUserProfile(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE personal user account
        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Roles = "Noob, Elite, Admin")]
        public async Task<IActionResult> DeleteUserAccount(string id)
        {
            return Ok();
        }

        //Upgrades and downgrades a user
        [HttpPatch("ChangeUserRole/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ChangeUserRole(string id)
        {
            return Ok();
        }
    }
}
