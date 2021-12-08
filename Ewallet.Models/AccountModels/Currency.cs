using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.Models.AccountModels
{
    public class Currency
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public bool IsMain { get; set; }
        public  decimal Balance { get; set; }
        public string WalletId { get; set; }
        public WalletModel Wallet { get; set; }
    }
}
