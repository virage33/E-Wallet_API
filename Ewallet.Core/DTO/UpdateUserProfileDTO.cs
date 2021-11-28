using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ewallet.Core.DTO
{
    public class UpdateUserProfileDTO
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Display(Name ="Phone No.")]
        public string PhoneNumber { get; set; }
    }
}
