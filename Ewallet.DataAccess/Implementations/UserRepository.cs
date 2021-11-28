using Ewallet.DataAccess.Interfaces;
using EwalletApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Implementations
{
    class UserRepository : IUserRepository
    {
        List<UserModel> use = new List<UserModel>();
        public Task<UserModel> CreateUser(UserModel user)
        {
            
            if (!InMemDb.Db.ContainsKey(user.Email))
            {
                InMemDb.Db.Add(user.Email, user);
            }
            else
            {
                
            }
            return Task.FromResult<UserModel>(InMemDb.Db[user.Email]); 
        }

        public Task DeleteUser(string Uid)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetByRole(string Role)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetByUserId(string Uid)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserProfile()
        {
            throw new NotImplementedException();
        }

       
    }
}
