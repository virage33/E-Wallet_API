using System;

namespace EwalletApi.Models.AccountModels
{
    public class Withdrawals
    {
        public int Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime Date { get; set; }
        public string AccountName { get; set;}
        public string TransactionId { get; set; }

    }
}