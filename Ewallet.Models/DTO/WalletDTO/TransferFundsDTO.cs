using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ewallet.Models.DTO.WalletDTO
{
    public class TransferFundsDTO
    {
        [Required]
        public decimal Amount { get; set; }
        [Display(Name = "Wallet Address")]
        [Required]
        public string walletId { get; set; }
        public string comment { get; set; }
        public string SenderName { get; set; }
    }
}
