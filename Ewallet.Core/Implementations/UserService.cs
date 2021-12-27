using Ewallet.Core.Interfaces;
using Ewallet.DataAccess.EntityFramework.Interfaces;
using Ewallet.Models;
//using Ewallet.DataAccess.Interfaces;
using Ewallet.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ewallet.Core.Implementations
{
    public class UserService : IUserService
    {
        private  IUserRepository UserRepository { get; set; }
        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        //Gets all users
        public async Task<List<UserDTO>> GetAllUsers()
        {
            List<UserDTO> result = new List<UserDTO>();
            var response = await UserRepository.GetAllUsers();
            response.ForEach((x) => {
                UserDTO user=new UserDTO();
                user.uid = x.Id;
                user.FirstName = x.FirstName;
                user.LastName = x.LastName;
                user.Email = x.Email;
                user.PhoneNumber = x.PhoneNumber;
                result.Add(user);
            });
            return result;
        }

        //gets user by name
        public async Task<List<UserDTO>> GetUsersByName(string name)
        {
            List<UserDTO> result = new List<UserDTO>();
            var response = await UserRepository.GetUserByName(name);
            if (response != null)
            {
                //response.ForEach((x) => {
                    UserDTO user = new UserDTO();
                    user.uid = response.Id;
                    user.FirstName = response.FirstName;
                    user.LastName = response.LastName;
                    user.PhoneNumber = response.PhoneNumber;
                    user.Email = response.Email;
                    result.Add(user);
                //});
            }

            return result;
        }

        //Get users by role
        public Task<List<UserDTO>> GetUsersByRole(string role)
        {
            throw new NotImplementedException();
        }

        //Get a user by id
        public async Task<UserDTO> GetUserById(string uid)
        {
            UserDTO result = new UserDTO();
            var response = await UserRepository.GetUserById(uid);
            if (response != null)
            {
                result.uid = response.Id;
                result.Email = response.Email;
                result.FirstName = response.FirstName;
                result.LastName = response.LastName;
                result.PhoneNumber = response.PhoneNumber;                    
                
            }
            return result;
        }

        //Delete a user
        public async Task<string> DeleteUser(string uid)

        {
            var res = await GetUserById(uid);
            AppUser user = new AppUser();
            user.Id = res.uid;
            user.LastName = res.LastName;
            user.Email = res.Email;
            user.FirstName = res.FirstName;
            var response = await UserRepository.DeleteUser(user);
            if (response.Succeeded!=false)
                return "successful";
            return "error";
        }

        public Task UpdateUser(string uid,UpdateUserProfileDTO data)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DeActivateUser(string uid)
        {
            var response = await UserRepository.ActivateOrDeActivateUser(false,uid);
            if (response.Succeeded !=false)
                return "Deactivated";
            return "error";
        }

        public async Task<string> ReActivateUser(string uid)
        {
            var response = await UserRepository.ActivateOrDeActivateUser(true, uid);
            if (response.Succeeded != false)
                return "Activated";
            return "error";
        }

        public async Task<string> ChangeUserRole(string id, string role)
        {
            
            return "successful";
        }

        public async Task<IList<string>> GetUserRoles(string uid)
        {
            var user = await UserRepository.GetUserById(uid);
            var res = await UserRepository.GetUserRoles(user);
            return res;
        }
    }
}
