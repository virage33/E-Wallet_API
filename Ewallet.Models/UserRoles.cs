using EwalletApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ewallet.Models
{
    public class UserRoles
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public UserModel User { get; set; }
        [Required]
        public  int RolesId { get; set; }
        public Roles Roles { get; set; }

    }
}
