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
    [Authorize]
    public class CurrencyConverterController : ControllerBase
    {
        // GET: api/<CurrencyConverterController>
        [HttpGet("Result")]
        public IActionResult Get()
        {
            return Ok();
        }

      
        // POST api/<CurrencyConverterController>
        [HttpPost("Convert")]
        public IActionResult Post([FromBody] int value)
        {
            return Ok();
        }

    }
}
