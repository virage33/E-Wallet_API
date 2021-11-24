using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EwalletApi.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {

        // GET: api/<UserController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Noob , Elite , Admin")]
        public string Get(int id)
        {
            return "value";
        }

        
        // PUT api/<UserController>/5
        [HttpPatch("{id}")]
        [Authorize(Roles = "Noob , Elite , Admin")]
        public void UpdateUserProfile(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Noob, Elite, Admin")]
        public void DeleteUserAccount(string id)
        {
        }

        [HttpPatch("{id}")]
        [Authorize(Roles ="Admin")]
        public void ChangeUserRole(string id)
        {

        }
    }
}
