using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Models.DTO
{
    public class CurrencyDTO
    {
       
        public string Id { get; set; }
        public bool IsMain { get; set; }
        public string WalletId { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public decimal Balance { get; set; }
        
    }
}
