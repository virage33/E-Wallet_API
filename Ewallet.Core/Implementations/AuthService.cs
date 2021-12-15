﻿
using Ewallet.Core.Interfaces;
using Ewallet.Core.JWT.Interfaces;
using System.Collections.Generic;
using Ewallet.DataAccess.EntityFramework.Interfaces;
//using Ewallet.DataAccess.Interfaces;
using Ewallet.Models.DTO;
using EwalletApi.Models;
using System;
using System.Threading.Tasks;
using Ewallet.Models;

namespace Ewallet.Core.Implementations
{
    public class AuthService : IAuthService
    {
        private IUserRepository UserRepository { get; set; }
        private readonly IJwtService _jwt;
        private readonly IWalletServices _walletService;

        public AuthService(IUserRepository userRepository, IJwtService jwt, IWalletServices walletServices)
        {
            UserRepository = userRepository;
            _jwt = jwt;
            _walletService = walletServices;
        }


        //Logs in the user and calls the jwt key generator
        public async Task<string> Login(LoginDTO credentials)
        {
            //List<string> ro = new List<string>() { "noob","elite" };
            var response = await UserRepository.GetUserByEmail(credentials.Email);
            if (response == null)
                return "Wrong email or Password";
           
            if (response.password != credentials.Password.Trim())
                return "Wrong password";
            
            if (response.IsActive is false)
                return "Deactivated Account";

            // var roles = await UserRepository.GetUserRoles(response.UserId);
            var roles = new List<string>{ "elite","noob"}; 
            if (roles.Count <1)
                return "user has no role";
            return _jwt.GenerateToken(response, roles);
        }
       

        //Registers a new user
        public async Task<string> Register(RegisterDTO details)
        {
            
                
            AppUser user = new AppUser();
            user.Email = details.Email;
            user.FirstName = details.FirstName;
            user.LastName = details.LastName;
            user.password = details.Password;
            user.PhoneNumber = details.PhoneNumber;
            
            
            
                   
            //if response is 1 send email...
            var response = await UserRepository.CreateUser(user, details.Role);

            if (response.Succeeded == false)
                return "exists";
            
            if (response.Succeeded == true)
            {
                var res = await _walletService.CreateWallet(user.Id, details.MainWalletCurrency);
                if (res == "error")
                    return "error creating wallet";
                return "successful";
            }
                
            return "error";
                           
        }

        //Logs out a user
        public void LogOut()
        {
            throw new NotImplementedException();
        }

        public async Task<string> ForgotPassword(ForgotPasswordDTO details)
        {
            var response = await UserRepository.GetUserByEmail(details.email);
            if (response != null)
                return response.password;
            return "0";
        }
    }
}
