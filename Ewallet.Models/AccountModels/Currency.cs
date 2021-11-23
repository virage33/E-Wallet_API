using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.Models.AccountModels
{
    public class Currency
    {
        
        public CurrencyType Type { get; set; }
        public string Symbol { get; set; }
        public string CurrencyId { get; set; }
    }
}
