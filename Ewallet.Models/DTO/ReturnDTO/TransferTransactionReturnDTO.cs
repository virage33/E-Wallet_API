using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Models.DTO.ReturnDTO
{
    public class TransferTransactionReturnDTO
    {
        public string TransactionsId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Remark { get; set; }
        public string TransactionType { get; set; }
        public string CurrencyShortCode { get; set; }
        public string WalletCurrencyid { get; set; }
        public string WalletId { get; set; }
        public string BeneficiaryWalletAddress { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryId { get; set; }
        public string SenderWalletAddress { get; set; }
    }
}
