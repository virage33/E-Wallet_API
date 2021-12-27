﻿using Ewallet.DataAccess.EntityFramework.Interfaces;
using Ewallet.Models;
using EwalletApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.EntityFramework.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> ActivateOrDeActivateUser(bool data, string uid)
        {
            var res = await userManager.FindByIdAsync(uid);
            IdentityResult response = null;
            if (res != null)
            {
                res.IsActive = data;
                response = await UpdateUserProfile(res);
                
            }
            return response;
        }

        public async Task<IdentityResult> CreateUser(AppUser user, string role)
        {
            user.UserName = user.Email;
            var res = await userManager.CreateAsync(user, user.password);
            IdentityResult response = null;
            if (res.Succeeded)
            {
                response = await userManager.AddToRoleAsync(user, role);
            }
            
            return response;

        }

        public async Task<IdentityResult> DeleteUser(AppUser user)
        {
             var response =await userManager.DeleteAsync(user);
            return response;
        }

        

        public Task<List<AppUser>> GetAllUsers()
        {
            List<AppUser> response = new List<AppUser>();
            var res = userManager.Users;
            foreach (var item in res)
            {
                response.Add(item);
            }
            return Task.FromResult<List<AppUser>>(response);
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            var response = await userManager.FindByEmailAsync(email);
            return response;
        }

        public async Task<AppUser> GetUserById(string Uid)
        {
            var response =  await userManager.FindByIdAsync(Uid);
            return response;
        }

        public async Task<AppUser> GetUserByName(string username)
        {
            var response = await userManager.FindByNameAsync(username);
            return response;
        }

       

        public async Task<IdentityResult> UpdateUserProfile(AppUser user)
        {
            var response = await userManager.UpdateAsync(user);
            
            return response;
        }

        public async Task<IList<string>> GetUserRoles(AppUser user)
        {
            var res = await userManager.GetRolesAsync(user);
            return res;
        }
    }
}
