using EwalletApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Models
{
    public class Roles
    {
        public string Id { get; set;}
        public string Role { get; set;}

        public UserModel user { get; set; }
    }
}
