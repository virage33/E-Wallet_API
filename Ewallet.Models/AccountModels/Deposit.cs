using System;

namespace EwalletApi.Models.AccountModels
{
    public class Deposit
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string WalletAddress { get; set; }
        public string TransactionId { get; set; }
        public Transactions Transactions { get; set; }

        public Deposit()
        {
            Date = DateTime.Now;
            TransactionId = new Guid().ToString();
        }
    }
}