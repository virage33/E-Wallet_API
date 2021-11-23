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
        private readonly IJwtService jwt;

        public AuthorizationController(IJwtService _jwt)
        {
            jwt = _jwt;
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return Ok(jwt.GenerateToken());
        }
        [HttpGet]
        [Authorize]
        public IActionResult Authorize()
        {
            return Ok("Authorized");
        }
    }
}
