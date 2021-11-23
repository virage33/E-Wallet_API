using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.Models.DTO
{
    public class ChangePasswordDTO
    {
        [Required]
        public string PreviousPassword { get; set; }
        [Required]
        public string NewPassword{ get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
