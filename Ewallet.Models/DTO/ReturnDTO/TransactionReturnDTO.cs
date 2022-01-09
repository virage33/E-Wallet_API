using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Models.DTO.ReturnDTO
{
    public class TransactionReturnDTO
    {
        public string TransactionsId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Remark { get; set; }
        public string TransactionType { get; set; }
        public string CurrencyShortCode { get; set; }
        public string WalletCurrencyid { get; set; }
        public string WalletId { get; set; }
    }
}
