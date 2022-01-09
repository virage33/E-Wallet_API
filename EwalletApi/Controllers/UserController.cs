using Ewallet.Commons;
using Ewallet.Core.Interfaces;
using Ewallet.Models;
using Ewallet.Models.DTO;
using Ewallet.Models.DTO.ReturnDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;



namespace EwalletApi.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;

        private IUserService UserService { get; set; }
        public UserController(IUserService userService, UserManager<AppUser> userManager, IAuthService authService)
        {
            UserService = userService;
            this._userManager = userManager;
            this._authService = authService;
        }


         
        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get(int page, int perPage)
        {

            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var listOfUserToReturn = new List<UserDTO>();
            var result = await UserService.GetAllUsers();
            if(result!= null)
            {
                var pagedList = PagedList<UserDTO>.Paginate(result.Data, page, perPage);
                foreach(var user in pagedList.Data)
                {
                    listOfUserToReturn.Add(user);
                }

                var res = new PaginatedListDTO<UserDTO>
                {
                    MetaData = pagedList.MetaData,
                    Data = listOfUserToReturn,
                };
                return Ok(res);
            }
            return BadRequest("no result");
        }

     
        [HttpGet("GetProfile/{id}")]
        [Authorize(Roles = "noob , elite , admin")]
        public async Task<IActionResult> GetProfile(string id)
        {
            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var result = await UserService.GetUserById(id);
            if (!result.IsSuccessful)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpGet("GetUsersByName")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsersByName(string name)
        {
            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var result = await UserService.GetUsersByName(name);
            if (!result.IsSuccessful)
                return BadRequest(result.Message);
            return Ok(result);
        }

        
        [HttpPatch("UpdateUserProfile/{id}")]
        [Authorize(Roles = "noob , elite , admin")]
        public async Task<IActionResult> UpdateUserProfile(string uid, [FromBody] UpdateUserProfileDTO value)
        {
            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var res = await UserService.UpdateUser(uid,value);
            if (!res.IsSuccessful)
                return BadRequest(res.Message);
            return Ok(res);
        }

        
        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUserAccount(string id)
        {
            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var response = await UserService.DeleteUser(id);
            if(!response.IsSuccessful) 
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        
        [HttpPatch("ChangeUserRole/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ChangeUserRole(string id,string role)
        {
            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isAdmin = HttpContext.User.IsInRole("admin");

            if (!isAdmin)
                return BadRequest("Access Denied");

            var res = await UserService.ChangeUserRole(id, role);
            if (!res.IsSuccessful)
                return BadRequest(res.Message);
           
            return Ok(res);
        }

        
        [HttpPatch("DeactivateUser")]
        [Authorize(Roles = "admin, noob, elite")]
        public async Task<IActionResult> DeactivateUser(string uid)
        {
            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isAdmin = HttpContext.User.IsInRole("admin");

            if (!isAdmin && loggedInUser != uid)
                return BadRequest("Access Denied");

            var response= await UserService.DeActivateUser(uid);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        
        [HttpPatch("ActivateUser")]
        [Authorize(Roles = "admin, noob, elite")]
        public async Task<IActionResult> ActivateUser(string uid)
        {
            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isAdmin = currentUser.IsInRole("admin"); ;

            if (!isAdmin && loggedInUser != uid)
                return BadRequest("Access Denied");

            var response = await UserService.ReActivateUser(uid);
            
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }
    }
}
