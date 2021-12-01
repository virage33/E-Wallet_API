using EwalletApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        UserModel GetUserByEmail(string email);
        UserModel GetUserById(string Uid);
        UserModel GetUserByName(string username);
        Task<List<UserModel>> GetAllUsers();
        UserModel GetUserByRole(string Role);

        Task<int> CreateUser(UserModel user);
        Task<int> DeleteUser(string Uid);
        Task<int> UpdateUserProfile();
    }
}
