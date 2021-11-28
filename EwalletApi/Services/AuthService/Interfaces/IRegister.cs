using Ewallet.Core.DTO;


namespace EwalletApi.Services.AuthService.Interfaces
{
    interface IRegister
    {
        void RegisterUser(RegisterDTO data);
    }
}
