using Ewallet.Core.Interfaces;
using Ewallet.Models.DTO;
using Ewallet.Models.DTO.ReturnDTO;
using Ewallet.Models.DTO.WalletDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EwalletApi.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class TransactionController : ControllerBase
    {
        private readonly ITransaction _transactionService;
        private readonly IAuthService _authService;

        public TransactionController(ITransaction transactionService, IAuthService authService)
        {
            this._transactionService = transactionService;
            this._authService = authService;
        }
        
        [HttpGet("GetAllTransactions")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var response = await _transactionService.GetAllTransactions();
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpGet("GetCurrencyCreditTransactions")]
        [Authorize(Roles =("admin,noob,elite"))]
        public async Task<IActionResult> GetAllCurrencyCreditTransactions(string currencyId)
        {
            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var response = await _transactionService.GetAllCurrencyCreditTransactions(currencyId);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        

        [HttpGet("GetWalletTransactions")]
        public async Task<IActionResult> GetAllWalletTransactions(string walletid)
        {

            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var response = await _transactionService.GetAllWalletTransactions(walletid);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpGet("Gettransaction")]
        public async Task<IActionResult> GetTransaction(string transactonId)
        {

            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var response = await _transactionService.GetTransaction(transactonId);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpGet("GetCurrencyTransactions")]
        public async Task<IActionResult> GetAllCurrencyTransactions(string currencyId)
        {

            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var response = await _transactionService.GetAllCurrencyTransactions(currencyId);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        //admin only
        [HttpGet("GetAllCreditTransactions")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> GetAllCreditTransactions()
        {

            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();


            var response = await _transactionService.GetAllCreditTransactions();
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        //get wallet credit transactions
        [HttpGet("GetWalletCreditTransactions")]
        public async Task<IActionResult> GetAllWalletCreditTransactions(string walletid)
        {

            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var response = await _transactionService.GetAllWalletCreditTransactions(walletid);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        //admin only
        [HttpGet("GetAllDebitTransactions")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> GetAllDebitTransactions()
        {
            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var response = await _transactionService.GetAllDebitTransactions();
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }
        
        [HttpGet("GetWalletDebitTransactions")]
        public async Task<IActionResult> GetAllWalletDebitTransactions(string walletid)
        {

            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var response = await _transactionService.GetAllWalletDebitTransactions(walletid);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpGet("GetCurrencyDebitTransactions")]
        public async Task<IActionResult> GetAllCurrencyDebitTransactions(string currencyId)
        {

            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var response = await _transactionService.GetAllCurrencyDebitTransactions(currencyId);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }
        
        //admin only
        [HttpGet("GetAllTransferTransactions")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> GetAllTransferTransactions()
        {

            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var response = await _transactionService.GetAllTransferTransactions();
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpGet("GetWalletTransferTransactions")]
        public async Task<IActionResult> GetAllWalletTransferTransactions(string walletid)
        {

            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var response = await _transactionService.GetAllWalletTransferTransactions(walletid);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpGet("GetCurrencyTranferTransactions")]
        public async Task<IActionResult> GetAllCurrencyTransferTransactions(string currencyId)
        {

            var userToken = HttpContext.Request.Headers["Authorization"];
            var blacklisted = await _authService.IsTokenblacklisted(userToken);
            if (blacklisted)
                return NotFound();

            var response = await _transactionService.GetAllCurrencyTransferTransactions(currencyId);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpDelete("ClearTransactionHistory")]
        public IActionResult ClearTransactionHistory()
        {
            return Ok();
        }

    }
}
