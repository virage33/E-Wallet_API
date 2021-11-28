using EwalletApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Interfaces
{
    interface IUserRepository
    {
        Task<UserModel> GetByEmail(string email);
        Task<UserModel> GetByUserId(string Uid);
        Task<UserModel> GetByUserName(string username);
        Task<UserModel> GetAllUsers();
        Task<UserModel> GetByRole(string Role);

        Task<UserModel> CreateUser(UserModel user);
        Task DeleteUser(string Uid);
        Task UpdateUserProfile();
    }
}
