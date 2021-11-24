using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.Models.AccountModels
{
    public class Currency
    {
        
        public string Type { get; set; }
        public string Code { get; set; }
        public string CurrencyId { get; set; }
        public WalletModel Wallet { get; set; }
    }
}
