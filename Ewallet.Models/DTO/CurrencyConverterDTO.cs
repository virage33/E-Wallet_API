using System.ComponentModel.DataAnnotations;


namespace Ewallet.Models.DTO
{
    public class CurrencyConverterDTO
    {
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public decimal amount { get; set; }
    }
}