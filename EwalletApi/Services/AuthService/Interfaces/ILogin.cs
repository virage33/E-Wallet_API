using Ewallet.Core.DTO;


namespace EwalletApi.Services.AuthService.Interfaces
{
    public interface ILogin
    {
        string LogIn(LoginDTO credentials); 
    }
}
