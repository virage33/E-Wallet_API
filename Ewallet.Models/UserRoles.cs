using EwalletApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Models
{
    public class UserRoles
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public UserModel User { get; set; }
        public  string RoleID { get; set; }
        public Roles Roles { get; set; }

    }
}
