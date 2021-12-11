using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ewallet.Core.DTO.WalletDTO
{
    public class FundWalletDTO
    {
        [Required]
        public decimal Amount { get; set; }
        [Display(Name ="Wallet Address")]
        [Required]
        public string walletId { get; set;}
        public string comment { get; set; }
    }
}
