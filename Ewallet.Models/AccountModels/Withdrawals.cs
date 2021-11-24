using System;

namespace EwalletApi.Models.AccountModels
{
    public class Withdrawals
    {

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string AccountAddress { get; set; }
        public string TransactionId { get; set; }
        public Transactions Transactions { get; set; }

        public Withdrawals()
        {
            Date = DateTime.Now;
            TransactionId = new Guid().ToString();
        }
    }
}