using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ewallet.Models.AccountModels
{
    public class WalletCurrency
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column(TypeName ="decimal(18,2)")]
        public decimal Currencybalance { get; set; }
        [Required]
        public bool IsMain { get; set; }
        [Required]
        public string WalletId { get; set; }
        public WalletModel Wallet { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        

        public WalletCurrency()
        {
            
        }
    }
}
