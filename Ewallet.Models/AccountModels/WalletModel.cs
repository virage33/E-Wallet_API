using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.Models.AccountModels
{
    public class WalletModel
    {
        public string Id { get; set; }
        public string Address { get; }
        public Currency MainCurrency { get; set; } 
        public List< Currency> Currency { get; set; }
        public decimal WalletBalance { get; set;}
        public List<Transactions> Transactions { get; set; }
        public UserModel User { get; set; }

        public WalletModel()
        {
            Currency = new List<Currency>();
            Transactions = new List<Transactions>();
        }
    }
}
