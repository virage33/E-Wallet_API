using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ewallet.Models.DTO
{
    public class LoginDTO
    {
        [EmailAddress(ErrorMessage ="Please Enter a valid email")]
        [Required]
        public string Email { get; set; }
 
        [Required]
        public string Password { get; set; }
    }
}
