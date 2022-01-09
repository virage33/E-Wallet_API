using EwalletApi.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ewallet.Models
{
    public class AppUser:IdentityUser
    {
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "minimum of 6 characters")]
        public string password { get; set; }
       
        public bool IsActive { get; set; }
        public List<WalletModel> Wallet { get; set; }

        public AppUser()
        {
            Wallet = new List<WalletModel>();
        }
    }
}
