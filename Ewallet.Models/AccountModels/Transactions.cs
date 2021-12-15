using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EwalletApi.Models.AccountModels
{
    public class Transactions
    {
        [Key]
        public string Id { get; set; }
        public WalletModel Wallet { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string AccountAddress { get; set; }
#nullable enable     
        [Column(TypeName = "nvarchar(255)")]
        public string? Remark { get; set; }
#nullable disable  
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string TransactionType { get; set; }
                                                                                
        public Transactions()
        {
            Date = DateTime.Now;
        }

    }
}