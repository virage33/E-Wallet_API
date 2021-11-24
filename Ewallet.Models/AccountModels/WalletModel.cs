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
        public Currency Currency { get; set; }
        public decimal AccountBalance { get; set;}
        public Transactions Transactions { get; set; }
        public UserModel User { get; set; }

    }
}
