using Ewallet.Core.DTO;
using Ewallet.Core.Interfaces;
using Ewallet.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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
                response.ForEach((x) => {
                    UserDTO user = new UserDTO();
                    user.FirstName = x.FirstName;
                    user.LastName = x.LastName;
                    user.PhoneNumber = x.PhoneNumber;
                    user.Email = x.Email;
                    result.Add(user);
                });
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
            var response = await UserRepository.DeleteUser(uid);
            if (response >0)
                return "Successful";
            return "Error";
        }

        public Task UpdateUser(string uid,UpdateUserProfileDTO data)
        {
            throw new NotImplementedException();
        }

        public Task DeActivateUser(string uid)
        {
            throw new NotImplementedException();
        }

        public Task ReActivateUser(string uid)
        {
            throw new NotImplementedException();
        }
    }
}
