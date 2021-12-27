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

    //[Authorize(Roles = "noob, elite")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransaction _transactionService;

        public TransactionController(ITransaction transactionService)
        {
            this._transactionService = transactionService;
        }
        
        [HttpGet("GetAllTransactions")]
        //admin only
        public async Task<IActionResult> GetAllTransactions()
        {
            var response = await _transactionService.GetAllTransactions();
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpGet("GetCurrencyCreditTransactions")]
        //user admin
        public async Task<IActionResult> GetAllCurrencyCreditTransactions(string currencyId)
        {
            var response = await _transactionService.GetAllCurrencyCreditTransactions(currencyId);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        

        [HttpGet("GetWalletTransactions")]
        public async Task<IActionResult> GetAllWalletTransactions(string walletid)
        {
            var response = await _transactionService.GetAllWalletTransactions(walletid);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpGet("Gettransaction")]
        public async Task<IActionResult> GetTransaction(string transactonId)
        {
            var response = await _transactionService.GetTransaction(transactonId);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpGet("GetCurrencyTransactions")]
        public async Task<IActionResult> GetAllCurrencyTransactions(string currencyId)
        {
            var response = await _transactionService.GetAllCurrencyTransactions(currencyId);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        //admin only
        [HttpGet("GetAllCreditTransactions")]
        public async Task<IActionResult> GetAllCreditTransactions()
        {
            var response = await _transactionService.GetAllCreditTransactions();
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        //get wallet credit transactions
        [HttpGet("GetWalletCreditTransactions")]
        public async Task<IActionResult> GetAllWalletCreditTransactions(string walletid)
        {
            var response = await _transactionService.GetAllWalletCreditTransactions(walletid);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        //admin only
        [HttpGet("GetAllDebitTransactions")]
        public async Task<IActionResult> GetAllDebitTransactions()
        {
            var response = await _transactionService.GetAllDebitTransactions();
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }
        
        [HttpGet("GetWalletDebitTransactions")]
        public async Task<IActionResult> GetAllWalletDebitTransactions(string walletid)
        {
            var response = await _transactionService.GetAllWalletDebitTransactions(walletid);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpGet("GetCurrencyDebitTransactions")]
        public async Task<IActionResult> GetAllCurrencyDebitTransactions(string currencyId)
        {
            var response = await _transactionService.GetAllCurrencyDebitTransactions(currencyId);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }
        
        //admin only
        [HttpGet("GetAllTransferTransactions")]
        public async Task<IActionResult> GetAllTransferTransactions()
        {
            var response = await _transactionService.GetAllTransferTransactions();
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpGet("GetWalletTransferTransactions")]
        public async Task<IActionResult> GetAllWalletTransferTransactions(string walletid)
        {
            var response = await _transactionService.GetAllWalletTransferTransactions(walletid);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response);
        }

        [HttpGet("GetCurrencyTranferTransactions")]
        public async Task<IActionResult> GetAllCurrencyTransferTransactions(string currencyId)
        {
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
