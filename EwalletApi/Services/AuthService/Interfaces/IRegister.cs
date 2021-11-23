using EwalletApi.UI.DTO;

namespace EwalletApi.Services.AuthService.Interfaces
{
    interface IRegister
    {
        void RegisterUser(RegisterDTO data);
    }
}
