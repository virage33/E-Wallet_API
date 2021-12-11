using Ewallet.Core.DTO;
using Ewallet.Core.Interfaces;
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
   // [Authorize]
    public class CurrencyConverterController : ControllerBase
    {
        private readonly ICurrencyConversionService _conversionService;
        public CurrencyConverterController(ICurrencyConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        // GET: api/<CurrencyConverterController>
        [HttpGet("CurrentMarketPrices")]
        public async Task<IActionResult> CurrentMarketPrices()
        {
            var response = await _conversionService.GetMarketPrices();
            return Ok(response);
        }

      
        // POST api/<CurrencyConverterController>
        [HttpPost("ConversionRate")]
        public async Task<IActionResult> ConversionRate([FromBody] CurrencyConverterDTO data)
        {
            var response = await _conversionService.ConvertCurrency(data);
            return Ok(response);
        }

    }
}
