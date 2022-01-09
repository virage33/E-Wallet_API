using Ewallet.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.Models.AccountModels
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string Type { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(5)")]
        public string Code { get; set; }
        [Required]
        //public string WalletId { get; set; }
        public List<WalletCurrency> Wallet { get; set; }
        public Currency()
        {
            Wallet = new List<WalletCurrency>();
        }
    }
}
