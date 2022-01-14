
using Ewallet.Models.DTO;
using Ewallet.Models.DTO.ReturnDTO;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDTO<LoginReturnDTO>> Login(LoginDTO credentials);

        Task<string> Register(RegisterDTO details);

        Task<bool> LogOut(string token);

        Task<bool> IsTokenblacklisted(string token);
        Task<string> ForgotPassword(ForgotPasswordDTO details);
    }
}
