using Ewallet.Core.DTO;
using Ewallet.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Implementations
{
    class AuthService : IAuthService
    {
        public Task<bool> Login(LoginDTO credentials)
        {
            throw new NotImplementedException();
        }
       
        public Task<LoginDTO> Register(RegisterDTO details)
        {
            throw new NotImplementedException();
        }
    }
}
