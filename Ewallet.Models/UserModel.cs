using Ewallet.Models;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.Models
{
    public class UserModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string Email { get; set; }
        public string UserId { get; set; } 
        public List<Roles> Role { get; set; }
        public List<WalletModel> Wallet { get; set; }
        public UserModel()
        {
            UserId = Guid.NewGuid().ToString();
            Role = new List<Roles>();
            Wallet = new List<WalletModel>();
        }
    }
}
