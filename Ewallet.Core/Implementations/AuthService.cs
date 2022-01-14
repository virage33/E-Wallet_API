
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
using Ewallet.Commons;
using Microsoft.AspNetCore.Http;
using Ewallet.Models.DTO.ReturnDTO;

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
        public async Task<ResponseDTO<LoginReturnDTO>> Login(LoginDTO credentials)
        {
           
            var response = await UserRepository.GetUserByEmail(credentials.Email);
            if (response == null)
                return ResponseHelper.CreateResponse<LoginReturnDTO>(message: "wrong email or password", null, status: false);

            if (response.password != credentials.Password.Trim())
                return ResponseHelper.CreateResponse<LoginReturnDTO>(message: "wrong password", null, status: false);

            if (response.IsActive is false)
                return ResponseHelper.CreateResponse<LoginReturnDTO>(message: "Deactivated Account", null, status: false); ;

            var roles = await UserRepository.GetUserRoles(response);
           
            if (roles.Count <1)
                return ResponseHelper.CreateResponse<LoginReturnDTO>(message: "error something happened", null, status: false); 
            string jwtToken = _jwt.GenerateToken(response, roles);

            return ResponseHelper.CreateResponse<LoginReturnDTO>(message:"successful",new LoginReturnDTO {Token=jwtToken,uid=response.Id },status:true) ;
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
            user.IsActive = true;
            
            
                   
            //if response is 1 send email...
            var response = await UserRepository.CreateUser(user, details.Role);

            if (response.Succeeded == false)
                return "exists";
            
            if (response.Succeeded == true)
            {
                var res = await _walletService.CreateWallet(user.Id, details.MainWalletCurrency);
                if (!res.IsSuccessful )
                    return res.Message;
                return res.Message;
            }
                
            return "error";
                           
        }

        //Logs out a user
        public async Task<bool> LogOut(string userToken)
        {
            
            BlacklistedTokens token = new BlacklistedTokens();
            token.Token = userToken;
            var response = await UserRepository.BlackListUserAuthToken(token);
            if (response > 0)
                return true;
            return false;
        }

        public async Task<string> ForgotPassword(ForgotPasswordDTO details)
        {
            var response = await UserRepository.GetUserByEmail(details.email);
            if (response != null)
                return response.password;
            return "0";
        }

        public async Task<bool> IsTokenblacklisted(string token)
        {
            var response = await UserRepository.IsTokenBlacklisted(token);
            if (!response)
                return false;
            return true;
        }
    }
}
