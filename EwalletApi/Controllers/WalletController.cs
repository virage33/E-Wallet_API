using Ewallet.Commons;
using Ewallet.Core.Interfaces;
using Ewallet.Models;
using Ewallet.Models.DTO.InputDTO;
using Ewallet.Models.DTO.WalletDTO;
using Ewallet.Models.emailModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IMailService _mailService;

        public WalletController(IWalletServices walletServices, ICurrencyService currencyService, IMailService mailService)
        {
            _walletServices = walletServices;
            _currencyService = currencyService;
            this._mailService = mailService;
        }

        [HttpPost("email")]
        public async Task<IActionResult> Send ([FromForm] MailRequest request)
        {
            try
            {
                await _mailService.SendMailAsync(request);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }


        //Gets all user wallets
        [HttpGet("GetUserWallets")]
        [Authorize(Roles = "noob , elite,admin")]
        public async Task<IActionResult> GetUserWallets(string userId)
        {

            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isAdmin = HttpContext.User.IsInRole("admin");

            if (!isAdmin && loggedInUser != userId)
                return BadRequest("Access Denied");

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
        public async Task<IActionResult> GetIndividualWallet(string uid,string walletId)
        {
            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isAdmin = HttpContext.User.IsInRole("admin");

            if (!isAdmin && loggedInUser != uid)
                return BadRequest("Access Denied");

            if (String.IsNullOrWhiteSpace(walletId))
                return BadRequest("enter a valid wallet Id");
            var response = await _walletServices.GetWallet(walletId);
            if (response == null)
                return NoContent();
            return Ok(response);
        }


       //Deletes individual wallets
        [HttpDelete("DeleteWallet")]
        //[Authorize(Roles ="elite")]
        public async Task<IActionResult> DeleteWallet(string uid,string walletId)
        {
            

            if (String.IsNullOrWhiteSpace(walletId))
                return BadRequest("enter a valid wallet Id");

            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (loggedInUser != uid)
                return BadRequest("Access Denied");

            var response = await _walletServices.DeleteWallet(walletId);
            if (response == null)
                return NoContent();

            return Ok(response);
        }


        [HttpPost("AddWallet")]
        //[Authorize(Roles ="elite")]
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
        public async Task<IActionResult> Withdraw(DebitDTO details)
        {
            var response = await _currencyService.Withdraw(details);
            return Ok(response);
        }

        [HttpPost("Transfer")]
        public async Task<IActionResult> Withdraw(TransferFundsDTO details)
        {
            var response = await _currencyService.Transfer(details);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpGet("UserBalance")]
        public async Task<IActionResult> UserBalance(string uid)
        {
            var response = await _walletServices.UserBalance(uid);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }
    }
}
