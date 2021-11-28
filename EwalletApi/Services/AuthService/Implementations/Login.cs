using Ewallet.Core.DTO;
using EwalletApi.Services.AuthService.Interfaces;
using EwalletApi.UI.Services.AuthService.Interfaces;

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
