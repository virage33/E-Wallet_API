using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ewallet.Models.AccountModels
{
    public class TransferTransactions
    {
        [ForeignKey("Transactions")]
        public string TransferTransactionsId { get; set; }
        public string BeneficiaryWalletAddress { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryId { get; set; }
        public string SenderWalletAddress { get; set; }
        public virtual Transactions Transactions { get; set; }

    }
}
