using EwalletApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ewallet.Models
{
    public class Roles
    {
        [Key]
        public int Id { get; set;}
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string Role { get; set;}
        public List<UserRoles> Users { get; set; }

        public Roles()
        {
            Users = new List<UserRoles>();
        }
    }
}
