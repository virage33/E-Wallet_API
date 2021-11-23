using System.Collections.Generic;

namespace EwalletApi.Models.AccountModels
{
    public class Transactions
    {
        public string Id { get; set; }
        public ICollection<Withdrawals> Withdrawals { get; set; }
        public ICollection<Deposit> Deposit { get; set; }
        public WalletModel Wallet { get; set; }
        public Transactions()
        {
            Withdrawals = new List<Withdrawals>();
            Deposit = new List<Deposit>();
        }

    }
}