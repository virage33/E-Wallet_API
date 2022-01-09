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
        private readonly IAuthService _authService;

        public WalletController(IWalletServices walletServices, ICurrencyService currencyService, IMailService mailService, IAuthService authService)
        {

            _walletServices = walletServices;
            _currencyService = currencyService;
            this._mailService = mailService;
            this._authService = authService;
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


        
        [HttpGet("GetUserWallets")]
        [Authorize(Roles = "noob , elite,admin")]
        public async Task<IActionResult> GetUserWallets(string userId)
        {
            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();
            
            if (String.IsNullOrWhiteSpace(userId))
                return BadRequest("please enter a valid Uid");

            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isAdmin = HttpContext.User.IsInRole("admin");
            
            if (!isAdmin && loggedInUser != userId)
                return BadRequest("Access Denied");
            
            var response = await _walletServices.GetAllUserWallets(userId);
            if (response != null)
                return Ok(response);
            return BadRequest("User doesn't exist");

        }


       
        [HttpGet("GetWallet")]
        [Authorize(Roles = "elite, noob, admin")]
        public async Task<IActionResult> GetIndividualWallet(string uid,string walletId)
        {

            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();


            if (String.IsNullOrWhiteSpace(walletId))
                return BadRequest("enter a valid wallet Id");

            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isAdmin = HttpContext.User.IsInRole("admin");

            if (!isAdmin && loggedInUser != uid)
                return BadRequest("Access Denied");

            var response = await _walletServices.GetWallet(walletId);
            if (response == null)
                return NoContent();
            return Ok(response);
        }


       //Deletes individual wallets
        [HttpDelete("DeleteWallet")]
        [Authorize(Roles ="elite")]
        public async Task<IActionResult> DeleteWallet(string uid,string walletId)
        {


            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

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
        [Authorize(Roles ="elite, admin")]
        public async Task<IActionResult> AddWallet(string uid , string mainCurrency)
        {

            //var userToken = HttpContext.Request.Headers["Authorization"];
            //var blacklisted = await _authService.IsTokenblacklisted(userToken);
            //if (blacklisted)
            //    return NotFound();

            if (String.IsNullOrWhiteSpace(uid))
                return BadRequest("enter a valid user Id");
            if (String.IsNullOrWhiteSpace(mainCurrency))
                return BadRequest("enter a valid currency");

            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isAdmin = HttpContext.User.IsInRole("admin");

            if (!isAdmin && loggedInUser != uid)
                return BadRequest("Access Denied");


            var response = await _walletServices.CreateWallet(uid,mainCurrency);
            if (response == null)
                return BadRequest("Something went wrong");
            return Ok(response);
        }


        [HttpPost("AddCurrency")]
        [Authorize("elite, admin")]
        public async Task<IActionResult> AddCurrency(string walletId, string currencyCode)
        {


            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            if (String.IsNullOrWhiteSpace(walletId))
                return BadRequest("enter a valid user Id");

            if (String.IsNullOrWhiteSpace(currencyCode))
                return BadRequest("enter a valid currency code");


            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isAdmin = HttpContext.User.IsInRole("admin");

            var UserWallet =await _walletServices.GetWallet(walletId);
            if (UserWallet == null)
                return BadRequest("Wallet doesn't exist");
            
            var isUserWallet = UserWallet.UserId == loggedInUser;

            if (!isAdmin && isUserWallet ==false)
                return BadRequest("Access Denied");

            var response = await _walletServices.AddCurrency(walletId, currencyCode);
            if (response == null)
                return BadRequest("Something went wrong");
            return Ok(response);
        }

        

        [HttpGet("GetAllWalletCurrencies")]
        [Authorize(Roles ="elite, noob, admin")]
        public async Task<IActionResult> GetAllWalletCurrencies(string walletId)
        {


            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            if (String.IsNullOrWhiteSpace(walletId))
                return BadRequest("please enter wallet Id");

            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isAdmin = HttpContext.User.IsInRole("admin");

            var UserWallet = await _walletServices.GetWallet(walletId);
            if (UserWallet == null)
                return BadRequest("Wallet doesn't exist");

            var isUserWallet = UserWallet.UserId == loggedInUser;

            if (!isAdmin && isUserWallet == false)
                return BadRequest("Access Denied");

            var response = await _currencyService.GetAllCurrencies(walletId);
            
            if (response.IsSuccessful != false)
                return Ok(response);
            return BadRequest(response);
        }




        [HttpGet("GetCurrency")]
        [Authorize(Roles ="noob,elite,admin")]
        public async Task<IActionResult> GetCurrency(string currencyId)
        {


            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            if (String.IsNullOrWhiteSpace(currencyId))
                return BadRequest("please enter a valid currency Id");

            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isAdmin = HttpContext.User.IsInRole("admin");

            var response = await _currencyService.GetCurrency(currencyId);

            if (response.IsSuccessful != false)
            {
                var userWallet = await _walletServices.GetWallet(response.Data.WalletId);
                if (!isAdmin && userWallet.UserId != loggedInUser)
                    return BadRequest("Access Denied");
                return Ok(response);
            }
            return BadRequest(response);
        }



        [HttpDelete("RemoveCurrency")]
        [Authorize (Roles ="elite,admin")]
        public async Task<IActionResult> RemoveCurrency(string currencyId)
        {


            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();


            if (String.IsNullOrWhiteSpace(currencyId))
                return BadRequest("please enter wallet Id");

            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isAdmin = currentUser.IsInRole("admin");

            var userCurrency = await _currencyService.GetCurrency(currencyId);
            if (!userCurrency.IsSuccessful)
                return BadRequest(userCurrency.Message);

            var userWallet = await _walletServices.GetWallet(userCurrency.Data.WalletId);
            if (!isAdmin && userWallet.UserId != loggedInUser)
                return BadRequest("Access Denied!");

            var response = await _currencyService.RemoveCurrency(currencyId);
            
            if (response.IsSuccessful != false)
                return Ok(response);
            return BadRequest(response);
        }



        [HttpPost("Deposit")]
        [Authorize(Roles = "noob,elite,admin")]
        public async Task<IActionResult> Deposit(string currencyId, decimal amount)
        {


            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isAdmin = currentUser.IsInRole("admin");

            var userCurrency = await _currencyService.GetCurrency(currencyId);
            if (!userCurrency.IsSuccessful)
                return BadRequest(userCurrency.Message);

            var userWallet = await _walletServices.GetWallet(userCurrency.Data.WalletId);
            if ( !isAdmin && userWallet.UserId != loggedInUser)
                return BadRequest("Access Denied!");

            var response = await _currencyService.Deposit(currencyId: currencyId, depositamount: amount);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }



        [HttpPost("Withdraw")]
        [Authorize(Roles ="noob,elite")]
        public async Task<IActionResult> Withdraw(DebitDTO details)
        {


            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userCurrency = await _currencyService.GetCurrency(details.CurrencyId);
            if (!userCurrency.IsSuccessful)
                return BadRequest(userCurrency.Message);

            var userWallet = await _walletServices.GetWallet(userCurrency.Data.WalletId);
            if (userWallet.UserId != loggedInUser)
                return BadRequest("Access Denied!");

            var response = await _currencyService.Withdraw(details);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }



        [HttpPost("Transfer")]
        [Authorize(Roles = "noob,elite")]
        public async Task<IActionResult> Transfer(TransferFundsDTO details)
        {


            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();


            ClaimsPrincipal currentUser = this.User;
            var loggedInUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userCurrency = await _currencyService.GetCurrency(details.SenderCurrencyAddress);
            if (!userCurrency.IsSuccessful)
                return BadRequest(userCurrency.Message);

            var userWallet = await _walletServices.GetWallet(userCurrency.Data.WalletId);
            if (userWallet.UserId != loggedInUser)
                return BadRequest("Access Denied!");

            var response = await _currencyService.Transfer(details);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }



        [HttpGet("UserBalance")]
        [Authorize(Roles = "noob,elite,admin")]
        public async Task<IActionResult> UserBalance(string uid)
        {


            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var response = await _walletServices.UserBalance(uid);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }


        
    }
}
