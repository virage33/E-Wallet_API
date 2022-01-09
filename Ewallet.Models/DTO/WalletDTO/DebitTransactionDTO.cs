using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Models.DTO.WalletDTO
{
    public class DebitTransactionDTO
    {
        public string TransactionsId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Remark { get; set; }
        public string TransactionType { get; set; }
        public string CurrencyShortCode { get; set; }
        public string WalletId { get; set; }
        public string WalletCurrencyId { get; set; }
        public string BeneficiaryAddress { get; set; }
        public string BeneficiaryName { get; set; }
        public string FinancialInstitutionType { get; set; }
        public string InstitutionName { get; set; }
    }
}
