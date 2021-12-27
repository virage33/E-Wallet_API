using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ewallet.Models.DTO.WalletDTO
{
    public class TransferFundsDTO
    {
        
        public decimal Amount { get; set; }
        public string SenderCurrencyAddress { get; set; }
        public string BeneficiaryCurrencyAddressId { get; set; }
        public string comment { get; set; }
        public string BeneficiaryName { get; set; }
    }
}
