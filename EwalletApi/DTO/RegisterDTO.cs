using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.UI.DTO
{
    public class RegisterDTO
    {
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Phone No.")]
        public int PhoneNumber { get; set; }
        [EmailAddress (ErrorMessage ="please enter a valid email")]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password{ get; set; }
        [Required]
        public string MainWalletCurrency { get; set; }


    }
}
