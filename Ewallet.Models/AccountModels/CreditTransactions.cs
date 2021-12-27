using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ewallet.Models.AccountModels
{
    public class CreditTransactions
    {
        [ForeignKey("Transactions")]
        public string CreditTransactionsId { get; set; }
        public virtual Transactions Transactions { get; set; }
        public string DestinationWalletAddress { get; set; }
        public string DestinationCurrencyAddress { get; set; }
        //public string SenderWalletAddress { get; set; }
        //public string BeneficiaryName { get; set; }
        //public string BeneficiaryId { get; set; }
    }
}
