using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Models.DTO.WalletDTO
{
    public class WalletBalanceDTO
    {
        public string CurrencyCode { get; set; }
        public decimal Balance { get; set; }
    }
}
