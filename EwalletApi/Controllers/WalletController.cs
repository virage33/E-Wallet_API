using Ewallet.Core.Interfaces;
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
   // [Authorize]
    public class WalletController : ControllerBase
    {
        private IWalletServices walletServices;
        public WalletController(IWalletServices _walletServices)
        {
            walletServices = _walletServices;
        }

        //Gets all user wallets
        [HttpGet("GetUserWallets")]
        //[Authorize(Roles = "Noob , Elite")]
        public async Task<IActionResult> GetUserWallets(string userId)
        {
            if (String.IsNullOrWhiteSpace(userId))
                return BadRequest("please enter a valid Uid");
            var response = await walletServices.GetAllUserWallets(userId);
            if (response != null)
                return Ok(response);
            return BadRequest("User doesn't exist");

        }

        // Gets an individual wallet
        [HttpGet("GetWallet")]
        public async Task<IActionResult> GetIndividualWallet(string walletId)
        {
            if (String.IsNullOrWhiteSpace(walletId))
                return BadRequest("enter a valid wallet Id");
            var response = await walletServices.GetWallet(walletId);
            if (response == null)
                return NoContent();
            return Ok(response);
        }


       //Deletes individual wallets
        [HttpDelete("DeleteWallet")]
       // [Authorize(Roles ="Elite")]
        public async Task<IActionResult> DeleteWallet(string walletId)
        {
            if (String.IsNullOrWhiteSpace(walletId))
                return BadRequest("enter a valid wallet Id");
            var response = await walletServices.DeleteWallet(walletId);
            if (response == null)
                return NoContent();

            return Ok(response);
        }

        [HttpPost("AddWallet")]
        public async Task<IActionResult> AddWallet(string uid , string mainCurrency)
        {
            if (String.IsNullOrWhiteSpace(uid))
                return BadRequest("enter a valid user Id");
            if (String.IsNullOrWhiteSpace(mainCurrency))
                return BadRequest("enter a valid currency");
            var response = await walletServices.CreateWallet(uid,mainCurrency);
            if (response == null)
                return BadRequest("Something went wrong");
            return Ok(response);
        }
    }
}
Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\hp\source\repos\EwalletApi\Ewallet.db\db.mdf; Integrated Security = True