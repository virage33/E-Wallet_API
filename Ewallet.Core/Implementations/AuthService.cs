using Ewallet.Core.DTO;
using Ewallet.Core.Interfaces;
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

        public AuthService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        public Task<bool> Login(LoginDTO credentials)
        {
            throw new NotImplementedException();
        }
       
        public string Register(RegisterDTO details)
        {
            UserModel user = new UserModel();
            user.Email = details.Email;
            user.FirstName = details.FirstName;
            user.LastName = details.LastName;
            user.password = details.Password;
            user.PhoneNumber = details.PhoneNumber;
            
            
            //if response is 1 send email...
            var response = UserRepository.CreateUser(user);

            if (response.Result == 2)
            {
                return "Email exists";
            }
            else if (response.Result == 3)
            {
                return "UserId Exists";
            }
            else if (response.Result == 1)
            {
                return "successful";
            }
            else
            {
                return "error";
            }
                
            
        }
    }
}
