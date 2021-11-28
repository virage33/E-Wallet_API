using System;
using System.Collections.Generic;

namespace EwalletApi.Models.AccountModels
{
    public class Transactions
    {
        public string Id { get; set; }
        public WalletModel Wallet { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string AccountAddress { get; set; }
        public string Remark { get; set; }
        public string TransactionType { get; set; }
                                                                                
        public Transactions()
        {
            Date = DateTime.Now;
        }

    }
}