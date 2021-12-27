using Ewallet.Models;
using EwalletApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.EntityFramework.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserByEmail(string email);
        Task<AppUser> GetUserById(string Uid);
        Task<AppUser> GetUserByName(string username);
        Task<List<AppUser>> GetAllUsers();
        Task<IdentityResult> ActivateOrDeActivateUser(bool data, string uid);
        Task<IdentityResult> CreateUser(AppUser user, string role);
        Task<IdentityResult> DeleteUser(AppUser user);
        Task<IdentityResult> UpdateUserProfile(AppUser user);
        Task<IList<string>> GetUserRoles(AppUser user);
        
    }
}
