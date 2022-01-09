using Ewallet.Models;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.Models
{
    public class UserModel:BaseEntity
    {
        [Key]
        public string UserId { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(30)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
#nullable enable
        [Column(TypeName = "nvarchar(15)")]
        public string? PhoneNumber { get; set; }
#nullable disable
        [Required]
        [MinLength(6,ErrorMessage ="minimum of 6 characters")]
        public string password { get; set; }
        [Required]
        [EmailAddress]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }
         
        public List<UserRoles> Role { get; set; }
        public List<WalletModel> Wallet { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        public UserModel()
        {
            UserId = Guid.NewGuid().ToString();
            Role = new List<UserRoles>();
            Wallet = new List<WalletModel>();
        }
    }
}
