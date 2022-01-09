using Ewallet.Commons;
using Ewallet.Core.Interfaces;

using Ewallet.DataAccess.EntityFramework.Interfaces;
using Ewallet.Models;
//using Ewallet.DataAccess.Interfaces;
using Ewallet.Models.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ewallet.Core.Implementations
{
    public class UserService : IUserService
    {

        private readonly UserManager<AppUser> _userManager;
        private  IUserRepository UserRepository { get; set; }
        public UserService(IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            UserRepository = userRepository;
            
            _userManager = userManager;
        }
      

        public async Task<ResponseDTO<List<UserDTO>>> GetAllUsers()
        {
            try
            {
                List<UserDTO> result = new List<UserDTO>();
                var response = await UserRepository.GetAllUsers();
                if (response.Count > 0)
                {
                    response.ForEach((x) =>
                    {
                        UserDTO user = new UserDTO();
                        user.uid = x.Id;
                        user.FirstName = x.FirstName;
                        user.LastName = x.LastName;
                        user.Email = x.Email;
                        user.PhoneNumber = x.PhoneNumber;
                        result.Add(user);
                    });
                    return ResponseHelper.CreateResponse<List<UserDTO>>(message: "successful", data: result, status: true);
                }
                return ResponseHelper.CreateResponse<List<UserDTO>>(message: "no records exist", data: null, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<List<UserDTO>>(message: "error", data: null, status: false,e);
            }
            
        }


        
        public async Task<ResponseDTO<List<UserDTO>>> GetUsersByName(string name)
        {
            try
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
                    return ResponseHelper.CreateResponse<List<UserDTO>>(message: "successful", data: result, status: true);
                }

                return ResponseHelper.CreateResponse<List<UserDTO>>(message: "record doesn't exist", data: null, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<List<UserDTO>>(message: "record doesn't exist", data: null, status: false,e);
            }
            
        }

        ////Get users by role
        //public Task<List<UserDTO>> GetUsersByRole(string role)
        //{
        //    throw new NotImplementedException();
        //}

       
        public async Task<ResponseDTO<UserDTO>> GetUserById(string uid)
        {
            try
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
                    return ResponseHelper.CreateResponse<UserDTO>(message: "successful", data: result, status: true);
                }
                return ResponseHelper.CreateResponse<UserDTO>(message: "record doesn't exist", data: null, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<UserDTO>(message: "successful", data: null, status: false,e);
            }
            
        }

        //Delete a user
        public async Task<ResponseDTO<string>> DeleteUser(string uid)
        {

            try
            {
                var res = await GetUserById(uid);
                AppUser user = new AppUser();
                user.Id = res.Data.uid;
                user.LastName = res.Data.LastName;
                user.Email = res.Data.Email;
                user.FirstName = res.Data.FirstName;
                var response = await UserRepository.DeleteUser(user);
                if (response.Succeeded != false)
                    return ResponseHelper.CreateResponse<string>(message: "successful", data: null, status: true);
                return ResponseHelper.CreateResponse<string>(message: "failed", data: null, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<string>(message: "error", data: null, status: false,e);
            }
           
        }

        public async Task <ResponseDTO<string>>UpdateUser(string uid,UpdateUserProfileDTO data)
        {
            try
            {
                var user = await UserRepository.GetUserById(uid);
                user.FirstName = data.FirstName;
                user.LastName = data.LastName;
                user.PhoneNumber = data.PhoneNumber;
                var res = await UserRepository.UpdateUserProfile(user);
                if (res.Succeeded)
                    return ResponseHelper.CreateResponse<string>(message: "successful", data: null, status: true);
                return ResponseHelper.CreateResponse<string>(message: "failed", data: null, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<string>(message: "error", data: null, status: false,e);
            }
            
        }

        public async Task<ResponseDTO<string>> DeActivateUser(string uid)
        {
            try
            {
                var response = await UserRepository.ActivateOrDeActivateUser(false, uid);
                if (response.Succeeded != false)
                    return ResponseHelper.CreateResponse<string>(message:"deactivated",data:null,status:true);
                return ResponseHelper.CreateResponse<string>(message: "failed", data: null, status: false); 

            }
            catch (Exception)
            {

                return ResponseHelper.CreateResponse<string>(message: "error", data: null, status: false);
            }
           
        }

        public async Task<ResponseDTO<string>> ReActivateUser(string uid)
        {
            try
            {

                var response = await UserRepository.ActivateOrDeActivateUser(true, uid);
                if (response.Succeeded != false)
                    return ResponseHelper.CreateResponse<string>(message: "activated", data: null, status: true);
                return ResponseHelper.CreateResponse<string>(message: "failed", data: null, status: false);
            }
            catch (Exception e)
            {

                return ResponseHelper.CreateResponse<string>(message: "error", data: null, status: false,e);
            }
        }

        public async Task<ResponseDTO<string>> ChangeUserRole(string id, string role)
        {
            try
            {
                var res = await UserRepository.ChangeUserRole(id, role);
                if (res > 0)
                {
                    return ResponseHelper.CreateResponse<string>(message: "successful", data: null, status: true);
                }
                return ResponseHelper.CreateResponse<string>(message: "failed", data: null, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<string>(message: "error", data: null, status: false, e);
            }
        }

        //public async Task<ResponseDTO<IList<string>>> GetUserRoles(string uid)
        //{
        //    try
        //    {
        //        var user = await UserRepository.GetUserById(uid);
        //        var res = await UserRepository.GetUserRoles(user);
        //        return ResponseHelper.CreateResponse<IList<string>>(message: "error", data: res, status: true);
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }
            
        //}
    }
}
