using Ewallet.Models;
using Ewallet.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.Models.AccountModels
{
    public class WalletModel:BaseEntity
    {


        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string MainCurrency { get; set; } 
        public List<WalletCurrency> Currency { get; set; }
        
        [Column(TypeName ="decimal(18,2)")]
        public decimal WalletBalance { get; set;}
        
        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public List<Transactions> Transactions { get; set; }

        public WalletModel()
        {
            Currency = new List<WalletCurrency>();
            Transactions = new List<Transactions>();
        }
    }
}
