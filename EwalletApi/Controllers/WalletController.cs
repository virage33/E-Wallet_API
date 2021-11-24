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
        // GET: api/<WalletController>
        [HttpGet]
        [Authorize(Roles = "Noob , Elite")]
        public IEnumerable<string> GetUserWallets(string userId)
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<WalletController>/5
        [HttpGet("{id}")]
        public string GetIndividualWallet(int walletId)
        {
            return "value";
        }

        // POST api/<WalletController>
        [HttpPost]
        [Authorize(Roles = "Noob , Elite, Admin")]
        public void FundWallet([FromBody] string value)
        {
        }

        // PUT api/<WalletController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WalletController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
