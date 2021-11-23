using System.Collections.Generic;

namespace EwalletApi.Models.AccountModels
{
    public class Transactions
    {
        public string Id { get; set; }
        public List<Withdrawals> Withdrawals { get; set; }
        public List<Deposit> Deposit { get; set; }

    }
}