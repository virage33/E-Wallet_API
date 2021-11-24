using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EwalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController : ControllerBase
    {
        //Gets all user wallets
        [HttpGet]
        [Authorize(Roles = "Noob , Elite")]
        public IEnumerable<string> GetUserWallets(string userId)
        {
            return new string[] { "value1", "value2" };
        }

        // Gets an individual wallet
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIndividualWallet(string walletId)
        {
            return Ok();
        }


       //Deletes individual wallets
        [HttpDelete("{id}")]
        [Authorize(Roles ="Elite")]
        public async Task<IActionResult> DeleteWallet(string walletId)
        {
            return Ok();
        }

        [HttpPost("AddWallet")]
        public async Task<IActionResult> AddWallet()
        {
            return Ok();
        }
    }
}
