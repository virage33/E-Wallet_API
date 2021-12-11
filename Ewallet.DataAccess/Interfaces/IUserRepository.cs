using EwalletApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByEmail(string email);
        Task<UserModel> GetUserById(string Uid);
        Task<List<UserModel>> GetUserByName(string username);
        Task<List<UserModel>> GetAllUsers();
        Task<List<UserModel>> GetUsersByRole(string Role);
        Task<int> ActivateOrDeActivateUser(bool data, string uid);
        Task<int> CreateUser(UserModel user, string role);
        Task<int> DeleteUser(string Uid);
        Task<int> UpdateUserProfile(UserModel user);
        Task<List<string>> GetUserRoles(string userId);
        Task<int> UserRole(string role,string uid);  
    }
}
