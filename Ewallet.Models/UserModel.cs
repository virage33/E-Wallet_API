using Ewallet.Models;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public Roles Role { get; set; }
        public List<WalletModel> Wallet { get; set; }
    }
}
