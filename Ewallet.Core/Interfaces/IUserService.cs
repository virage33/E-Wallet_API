using Ewallet.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsers();
        Task<List<UserDTO>> GetUsersByName(string name);
        Task<List<UserDTO>> GetUsersByRole(string role);
        Task<UserDTO> GetUserById(string uid);
        Task<string> DeleteUser(string uid);
        Task UpdateUser(string uid, UpdateUserProfileDTO data);
        Task<string> DeActivateUser(string uid);
        Task<string> ReActivateUser(string uid);
    }
}
