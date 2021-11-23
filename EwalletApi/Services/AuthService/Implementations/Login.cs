using EwalletApi.Services.AuthService.Interfaces;
using EwalletApi.UI.DTO;
using EwalletApi.UI.Services.AuthService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.UI.Services.AuthService.Implementations
{
    public class Login : ILogin
    {
        private readonly IJwtService _jwt; 
        public Login(IJwtService jwt)
        {
            _jwt = jwt;
        }
        public string LogIn(LoginDTO credentials)
        {

            return _jwt.GenerateToken(); 
        }
    }
}
