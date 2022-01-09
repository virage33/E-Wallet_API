using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ewallet.Models.AccountModels
{
    public class DebitTransactions
    {
        [ForeignKey("Transactions")]
        public string DebitTransactionsId { get; set; }
        [Required]
        public string BeneficiaryAddress { get; set; }
        public string BeneficiaryName { get; set; }
        public string FinancialInstitutionType { get; set; }
        public string InstitutionName { get; set; }
        public virtual Transactions Transactions { get; set; } 
    }
}
