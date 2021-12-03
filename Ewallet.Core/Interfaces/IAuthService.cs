using Ewallet.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginDTO credentials);

        Task<string> Register(RegisterDTO details);

        void LogOut();
    }
}
