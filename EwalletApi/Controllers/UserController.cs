﻿using Ewallet.Core.DTO;
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


        // GETs all users
        [HttpGet("GetAllUsers")]
        //[Authorize(Roles = "Admin")]
        public async Task<IEnumerable<UserDTO>> Get()
        {
            var result = await UserService.GetAllUsers();
            return result;
        }

        // GET personal user profile
        [HttpGet("GetProfile/{id}")]
       // [Authorize(Roles = "Noob , Elite , Admin")]
        public async Task<IActionResult> GetProfile(string id)
        {
            var result = await UserService.GetUserById(id);
            return Ok(result);
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
            var response = await UserService.DeleteUser(id);
            if(response == "error") 
                return BadRequest("unsuccessful");
            return Ok("Deleted!");
        }

        //Upgrades and downgrades a user
        [HttpPatch("ChangeUserRole/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ChangeUserRole(string id)
        {
           
            return Ok();
        }

        //Activate user
        [HttpPatch("DeactivateUser")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeactivateUser(string uid)
        {
            var response= await UserService.DeActivateUser(uid);
            if (response == "error")
                return BadRequest("Not Successful");
            return Ok("Deactivated");
        }

        //Activate user
        [HttpPatch("ActivateUser")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActivateUser(string uid)
        {
            var response = await UserService.ReActivateUser(uid);
            if (response == "error")
                return BadRequest("Not Successful");
            return Ok("Activated");
        }
    }
}