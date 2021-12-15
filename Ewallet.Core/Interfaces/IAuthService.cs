
using Ewallet.Models.DTO;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginDTO credentials);

        Task<string> Register(RegisterDTO details);

        void LogOut();
        Task<string> ForgotPassword(ForgotPasswordDTO details);
    }
}
