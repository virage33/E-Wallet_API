using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Models.DTO.WalletDTO
{
    public class WalletDTO
    {
        public string Id { get; set; } 
        public string MainCurrency { get; set; }
        public List<CurrencyDTO> Currency { get; set; }
        public decimal WalletBalance { get; set; }
        //public List<Transactions> Transactions { get; set; }
        public string UserId { get; set; }
        

        public WalletDTO()
        {
            Currency = new List<CurrencyDTO>();
            //Transactions = new List<Transactions>();
        }
    }
}
