using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ewallet.Core.DTO
{
    public class RegisterDTO
    {
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
       
        [Display(Name = "Phone No.")]
        [MaxLength(11,ErrorMessage ="Must be 11 characters long")]
        [MinLength(11,ErrorMessage ="Must be 11 characters long")]
        public string PhoneNumber { get; set; }

        [EmailAddress (ErrorMessage ="please enter a valid email")]
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6,ErrorMessage ="password must not be less than 6 characters")]
        public string Password{ get; set; }

        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name ="Main Wallet Currency")]
        public string MainWalletCurrency { get; set; }
        [Required]
        [Display(Name ="User Type")]
        public string Role { get; set; }


    }
}
