using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Models.DTO
{
    public class CreditTransactionDTO
    {
        public string TransactionsId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Remark { get; set; }
        public string TransactionType { get; set; }
        public string CurrencyShortCode { get; set; }
        public string WalletCurrencyId { get; set; }
        public string DestinationWalletAddress { get; set; }
        public string DestinationCurrencyAddress { get; set; }
    }
}
