using Ewallet.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Login(LoginDTO credentials);

        string Register(RegisterDTO details);
    }
}
