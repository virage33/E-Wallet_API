using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Models.DTO
{
    public class UserDTO
    {
        public string uid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //public List<UserRoles> Role { get; set; }
        //public List<WalletModel> Wallet { get; set; }
    }
}
