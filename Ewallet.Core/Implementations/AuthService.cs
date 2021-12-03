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

        public AuthService(IUserRepository userRepository, IJwtService jwt)
        {
            UserRepository = userRepository;
            _jwt = jwt;
        }


        //Logs in the user and calls the jwt key generator
        public async Task<string> Login(LoginDTO credentials)
        {
            var response = await UserRepository.GetUserByEmail(credentials.Email);
            if (response == null)
                return "Wrong email or Password";
            if (response.password != credentials.Password.Trim())
                return "Wrong password";
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
            {
                return "Email exists";
            }
            else if (response == 3)
            {
                return "UserId Exists";
            }
            else if (response == 1)
            {
                return "successful";
            }
            else
            {
                return "error";
            }
                           
        }

        //Logs out a user
        public void LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
