﻿using Ewallet.Models;
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
        [MinLength(6)]
        public string password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string UserId { get; set; } 
        public List<UserRoles> Role { get; set; }
        public List<WalletModel> Wallet { get; set; }
        public UserModel()
        {
            UserId = Guid.NewGuid().ToString();
            Role = new List<UserRoles>();
            Wallet = new List<WalletModel>();
        }
    }
}
