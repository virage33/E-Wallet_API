
using Ewallet.Core.DTO.WalletDTO;
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

    [Authorize(Roles = "noob, elite")]
    public class TransactionController : ControllerBase
    {
        
        [HttpGet("GetAllTransactions")]
        public IEnumerable<string> GetAllTransactions()
        {
            return new string[] { "value1", "value2" };
        }

       
        [HttpGet("WithdrawFunds/{id}")]
        public IActionResult WithdrawFunds(int id)
        {
            return Ok();
        }


        [HttpPost("TransferFunds")]
        public IActionResult TransferFunds([FromBody] TransferFundsDTO details)
        {
            return Ok();
        }


        [HttpPost("FundWallet")]
        [Authorize(Roles = "noob , elite, admin")]
        public IActionResult FundWallet([FromBody] FundWalletDTO details)
        {
            return Ok();
        }

        [HttpDelete("ClearTransactionHistory")]
        public IActionResult ClearTransactionHistory()
        {
            return Ok();
        }



    }
}
