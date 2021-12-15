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
        private readonly IWalletServices _walletServices;
        private readonly ICurrencyService _currencyService;
        public WalletController(IWalletServices walletServices, ICurrencyService currencyService)
        {
            _walletServices = walletServices;
            _currencyService = currencyService;
        }

        //Gets all user wallets
        [HttpGet("GetUserWallets")]
       // [Authorize(Roles = "noob , elite,admin")]
        public async Task<IActionResult> GetUserWallets(string userId)
        {
            if (String.IsNullOrWhiteSpace(userId))
                return BadRequest("please enter a valid Uid");
            var response = await _walletServices.GetAllUserWallets(userId);
            if (response != null)
                return Ok(response);
            return BadRequest("User doesn't exist");

        }


        // Gets an individual wallet
        [HttpGet("GetWallet")]
        //[Authorize(Roles = "elite, noob, admin")]
        public async Task<IActionResult> GetIndividualWallet(string walletId)
        {
            if (String.IsNullOrWhiteSpace(walletId))
                return BadRequest("enter a valid wallet Id");
            var response = await _walletServices.GetWallet(walletId);
            if (response == null)
                return NoContent();
            return Ok(response);
        }


       //Deletes individual wallets
        [HttpDelete("DeleteWallet")]
        [Authorize(Roles ="elite")]
        public async Task<IActionResult> DeleteWallet(string walletId)
        {
            if (String.IsNullOrWhiteSpace(walletId))
                return BadRequest("enter a valid wallet Id");
            var response = await _walletServices.DeleteWallet(walletId);
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
            var response = await _walletServices.CreateWallet(uid,mainCurrency);
            if (response == null)
                return BadRequest("Something went wrong");
            return Ok(response);
        }


        [HttpPost("AddCurrency")]
        public async Task<IActionResult> AddCurrency(string walletId, string currencyCode)
        {
            if (String.IsNullOrWhiteSpace(walletId))
                return BadRequest("enter a valid user Id");

            if (String.IsNullOrWhiteSpace(currencyCode))
                return BadRequest("enter a valid currency code");

            var response = await _walletServices.AddCurrency(walletId, currencyCode);
            if (response == null)
                return BadRequest("Something went wrong");
            return Ok(response);
        }

        [HttpGet("GetAllWalletCurrencies")]
        public async Task<IActionResult> GetAllWalletCurrencies(string walletId)
        {
            if (String.IsNullOrWhiteSpace(walletId))
                return BadRequest("please enter wallet Id");
            
            var response = await _currencyService.GetAllCurrencies(walletId);
            
            if (response.IsSuccessful != false)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("GetCurrency")]
        public async Task<IActionResult> GetCurrency(string currencyId)
        {
            if (String.IsNullOrWhiteSpace(currencyId))
                return BadRequest("please enter currency Id");
            
            var response = await _currencyService.GetCurrency(currencyId);
            
            if (response.IsSuccessful != false)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete("RemoveCurrency")]
        public async Task<IActionResult> RemoveCurrency(string currencyId)
        {
            if (String.IsNullOrWhiteSpace(currencyId))
                return BadRequest("please enter wallet Id");

            var response = await _currencyService.RemoveCurrency(currencyId);
            
            if (response.IsSuccessful != false)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit(string currencyId, decimal amount)
        {
            var response = await _currencyService.Deposit(currencyId: currencyId, depositamount: amount);
            return Ok(response);
        }

        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw(string currencyId, decimal amount)
        {
            var response = await _currencyService.Withdraw(currencyId: currencyId, amount:amount);
            return Ok(response);
        }
    }
}
