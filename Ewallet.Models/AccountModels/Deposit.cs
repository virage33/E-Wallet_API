using System;

namespace EwalletApi.Models.AccountModels
{
    public class Deposit
    {
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public Currency Currency { get; set; }
        public string AccountName { get; set; }
        public string TransactionId { get; set; }
        public Transactions Transactions { get; set; }
    }
}