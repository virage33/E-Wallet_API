using Ewallet.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDTO<List<UserDTO>>> GetAllUsers();
        Task<ResponseDTO<List<UserDTO>>> GetUsersByName(string name);
       // Task<ResponseDTO<List<UserDTO>>> GetUsersByRole(string role);
        Task<ResponseDTO<UserDTO>> GetUserById(string uid);
        Task<ResponseDTO<string>> DeleteUser(string uid);
        Task<ResponseDTO<string>> UpdateUser(string uid, UpdateUserProfileDTO data);
        Task<ResponseDTO<string>> DeActivateUser(string uid);
        Task<ResponseDTO<string>> ReActivateUser(string uid);
        //Task<ResponseDTO<IList<string>>> GetUserRoles(string uid);
        Task<ResponseDTO<string>> ChangeUserRole(string id, string role);
    }
}
