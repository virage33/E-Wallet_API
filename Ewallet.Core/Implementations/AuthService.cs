using Ewallet.Core.DTO;
using Ewallet.Core.Interfaces;
using Ewallet.Core.JWT.Interfaces;
using Ewallet.DataAccess.Interfaces;
using EwalletApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            var response = await UserRepository.GetUserByEmail(credentials.Email);
            if (response == null)
                return "Wrong email or Password";
            if (response.password != credentials.Password.Trim())
                return "Wrong password";
            if (response.IsActive is false)
                return "Deactivated Account";
            return _jwt.GenerateToken();
        }
       

        //Registers a new user
        public async Task<string> Register(RegisterDTO details)
        {
            
                
            UserModel user = new UserModel();
            user.Email = details.Email;
            user.FirstName = details.FirstName;
            user.LastName = details.LastName;
            user.password = details.Password;
            user.PhoneNumber = details.PhoneNumber;
                   
            //if response is 1 send email...
            var response = await UserRepository.CreateUser(user);

            if (response == 2)
                return "exists";
            
            if (response == 1)
            {
                var res = await _walletService.CreateWallet(user.UserId, details.MainWalletCurrency);
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

        public async Task<string> ForgotPassword(string email)
        {
            var response = await UserRepository.GetUserByEmail(email);
            if (response != null)
                return response.password;
            return "0";
        }
    }
}
