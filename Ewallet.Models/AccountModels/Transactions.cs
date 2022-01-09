using Ewallet.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EwalletApi.Models.AccountModels
{
    public class Transactions
    {
        [Key]
        public string TransactionsId { get; set; } = Guid.NewGuid().ToString();
        public string WalletId { get; set; }
        public WalletModel Wallet { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        public string Remark { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string TransactionType { get; set; }
        [Required]   
        public string CurrencyShortCode { get; set; }
        public string WalletCurrencyId { get; set; }
        public WalletCurrency WalletCurrency { get; set; }
        public virtual CreditTransactions CreditTransactions{ get; set; }
        public virtual DebitTransactions DebitTransactions { get; set; }
        public virtual TransferTransactions TransferTransactions { get; set; }


        public Transactions()
        {
            Date = DateTime.Now;
        }

    }
}