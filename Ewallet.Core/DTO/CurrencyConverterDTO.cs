using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ewallet.Core.DTO
{
    public class CurrencyConverterDTO
    {
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
    }
}
