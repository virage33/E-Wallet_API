using EwalletApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.DataAccess
{
    public class InMemDb
    {
        public UserModel _User = new UserModel();
        
        public static Dictionary<string, UserModel> Db = new Dictionary<string, UserModel>();
        public void add()
        {
            Models.UserRoles roles = new Models.UserRoles();
            roles.Roles.Role = "noob";
            roles.UserId = _User.UserId;
            _User.FirstName = "Suleiman";
            _User.LastName = "Sani";
            _User.Role.Add(roles);
            _User.password = "1234567";
            _User.Email = "a@gmail.com";
            Db.Add(_User.Email, _User);
        }
    }
}
