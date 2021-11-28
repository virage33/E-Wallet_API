using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ewallet.Core.DTO.WalletDTO
{
    public class WithdrawFundsDTO
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string destinationAddress { get; set; }
    }
}
