using Ewallet.DataAccess.Interfaces;
using EwalletApi.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {

        private readonly IConfiguration _config;
        private readonly SqlConnection _conn;

        public UserRepository(IConfiguration configuration)
        {
            _config = configuration;
            _conn = new SqlConnection(_config.GetSection("ConnectionStrings:Default").Value);

        }

        //Adds user to the database
        public async Task<int> CreateUser(UserModel user, string role)
        {
            string command = "INSERT INTO Users Values(@UserId,@FirstName,@LastName,@Email,@Password,@PhoneNumber,@PasswordHash,@IsActive,@DateCreated,@LastModified)";
            string isEmailExist = "Select Email from USERS WHERE Email = @Email";
            int response = 0;

            try
            {
               
                //checks if email already exists
                await using (var cmd= new SqlCommand(isEmailExist,_conn))
                {
                    _conn.Open();
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    var exist = cmd.ExecuteScalar();
                    if (exist != null)
                    {
                        _conn.Close();
                        return response = 2;
                    }
                    _conn.Close();
                }

                    
                    //creates new user
                using (var cmd = new SqlCommand(command, _conn))
                {
                    _conn.Open();
                    cmd.Parameters.AddWithValue("@UserId", user.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Password", user.password);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                    cmd.Parameters.AddWithValue("@DateCreated", user.Created);
                    cmd.Parameters.AddWithValue("@LastModified", user.LastModified);

                    response = (int)cmd.ExecuteNonQuery();
                    _conn.Close();
                }
                if (response > 0)
                {
                    await UserRole(role, user.UserId);
                }
              
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }

        //Deletes a user
        public async Task<int> DeleteUser(string Uid)
        {
            string command = "DELETE FROM Users WHERE UserId = @UserId";
            var response = 0;
            try
            {
              
                
                await using (var cmd = new SqlCommand(command, _conn))
                {
                    _conn.Open();
                    cmd.Parameters.AddWithValue("@UserId", Uid);
                    response = (int)cmd.ExecuteNonQuery();
                    _conn.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }


        //Gets all users
        public async Task<List<UserModel>> GetAllUsers()
        {
            string command = "SELECT * FROM Users";
            List<UserModel> result = new List<UserModel>();

            try
            {
             

                var cmd = new SqlCommand(command, _conn);
                    
                _conn.Open();

                await using (var response = cmd.ExecuteReader())
                {
                    
                    while (response.Read())
                    {
                        UserModel user = new UserModel();
                        //var data = response.GetString(response.GetOrdinal("UserId"));
                        user.FirstName = response.GetString(response.GetOrdinal("FirstName")).Trim();
                        user.LastName = response.GetString(response.GetOrdinal("LastName")).Trim();
                        user.Email = response.GetString(response.GetOrdinal("Email")).Trim();
                        user.password = response.GetString(response.GetOrdinal("Password")).Trim();
                        user.PhoneNumber = response.GetString(response.GetOrdinal("PhoneNumber")).Trim();
                        result.Add(user);
                    }

                }
                _conn.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
           

            return result;
        }


        //Gets user by email
        public async Task<UserModel> GetUserByEmail(string email)
        {
            string command = "SELECT * FROM Users WHERE Email=@Email";
            string isEmailExist = "Select Email from USERS WHERE Email = @Email";
            UserModel user = new UserModel();

            try
            {
               

                var cmd = new SqlCommand(command, _conn);
                cmd.Parameters.AddWithValue("@Email", email);
                //checks if email already exists
                await using (var cmd1 = new SqlCommand(isEmailExist, _conn))
                {
                    _conn.Open();
                    cmd1.Parameters.AddWithValue("@Email", email);
                    var exist = cmd1.ExecuteScalar();
                    if (exist == null)
                    {
                        _conn.Close();
                        return null;
                    }
                    _conn.Close();
                }
                _conn.Open();

                await using (var response = cmd.ExecuteReader())
                {
                  
                    while (response.Read())
                    {

                        user.UserId = response.GetString(response.GetOrdinal("UserId")).ToString();
                        user.FirstName = response.GetString(response.GetOrdinal("FirstName")).Trim();
                        user.LastName = response.GetString(response.GetOrdinal("LastName")).Trim();
                        user.Email = response.GetString(response.GetOrdinal("Email")).Trim();
                        user.password = response.GetString(response.GetOrdinal("Password")).Trim();
                        user.PhoneNumber = response.GetString(response.GetOrdinal("PhoneNumber")).Trim();
                        user.IsActive = response.GetBoolean(response.GetOrdinal("IsActive"));

                    }

                }

                _conn.Close();

            }
            catch (Exception)
            {

                throw;
            }
            return user;
        }


        public async Task<UserModel> GetUserById(string Uid)
        {
            string command = "SELECT * FROM Users WHERE UserId=@UserId";
            UserModel user = new UserModel();

            try
            {
                var cmd = new SqlCommand(command, _conn);
                cmd.Parameters.AddWithValue("@UserId", Uid);
                _conn.Open();

                await using (var response = cmd.ExecuteReader())
                {
                   
                    while (response.Read())
                    {

                        user.UserId = response.GetString(response.GetOrdinal("UserId"));
                        user.FirstName = response.GetString(response.GetOrdinal("FirstName")).Trim();
                        user.LastName = response.GetString(response.GetOrdinal("LastName")).Trim();
                        user.Email = response.GetString(response.GetOrdinal("Email")).Trim();
                        user.password = response.GetString(response.GetOrdinal("Password")).Trim();
                        user.PhoneNumber = response.GetString(response.GetOrdinal("PhoneNumber")).Trim();

                    }

                }
                _conn.Close();


            }
            catch (Exception)
            {

                throw;
            }
            return user;
        }



        //get users by name
        public async Task<List<UserModel>> GetUserByName(string username)
        {
            string command = "SELECT * FROM Users WHERE FirstName LIKE @Name OR LastName LIKE @Name";
            List<UserModel> result = new List<UserModel>();

            try
            {
                var cmd = new SqlCommand(command, _conn);
                cmd.Parameters.AddWithValue("@Name", username+"%");
                _conn.Open();

                using (var response = await cmd.ExecuteReaderAsync())
                {
                 
                    while (response.Read())
                    {
                        UserModel user = new UserModel();
                        user.UserId = response.GetString(response.GetOrdinal("UserId"));
                        user.FirstName = response["FirstName"].ToString().Trim();
                        user.LastName = response.GetString(response.GetOrdinal("LastName")).Trim();
                        user.Email = response.GetString(response.GetOrdinal("Email")).Trim();
                        user.password = response.GetString(response.GetOrdinal("Password")).Trim();
                        user.PhoneNumber = response.GetString(response.GetOrdinal("PhoneNumber")).Trim();
                        result.Add(user);
                    }

                }
                _conn.Close();


            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }



        //get users by role
        public Task<List<UserModel>> GetUsersByRole(string Role)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateUserProfile(UserModel user)
        {
            throw new NotImplementedException();
        }


        //Activate or Deactivate User
        public async Task<int> ActivateOrDeActivateUser(bool data, string uid)
        {
            int response = 0;
            string command = "UPDATE Users SET IsActive = @IsActive WHERE UserId = @UserId";
            try
            {
               await using (_conn)
                {
                    var cmd = new SqlCommand(command, _conn);
                    cmd.Parameters.AddWithValue("@IsActive", data);
                    cmd.Parameters.AddWithValue("@UserId", uid);
                    _conn.Open();
                    response = (int)cmd.ExecuteNonQuery();
                    _conn.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;

        }

        
        public async Task<int> ChangeUserRole(string uid, string role)
        {
            int response = 0;
            string command = "UPDATE Users SET IsActive = @IsActive WHERE UserId = @UserId";
            try
            {
                await using (_conn)
                {
                    var cmd = new SqlCommand(command, _conn);
                    cmd.Parameters.AddWithValue("@IsActive", @role);
                    cmd.Parameters.AddWithValue("@UserId", uid);
                    _conn.Open();
                    response = (int)cmd.ExecuteNonQuery();
                    _conn.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;

        }

        public async Task<List<string>> GetUserRoles(string userId)
        {
            List<string> roles = new List<string>();
            string command = "SELECT * FROM UserRoles JOIN Roles ON UserRoles.RoleId = Roles.Id WHERE UserId=@userId";
            try
            {
                var cmd = new SqlCommand(command, _conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                _conn.Open();
                using (var response = await cmd.ExecuteReaderAsync())
                {
                    if (response != null)
                    {
                        while (response.Read())
                        {
                            roles.Add(response["Roles"].ToString().ToLower().Trim());
                        }
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                _conn.Close();
            }
            return roles;
        }

        public async Task<int> UserRole(string role,string userId)
        {
            string cmd1 = "SELECT Id FROM Roles WHERE Roles = @role";
            string command = "INSERT INTO UserRoles Values(@RoleId,@UserId)";
            var roleId = "";
            var response2 = 0;
            try
            {

            
                using (var cmd = new SqlCommand(cmd1, _conn))
                {
                    cmd.Parameters.AddWithValue("@role", role.ToLower());
                    _conn.Open();
                    var response = await cmd.ExecuteReaderAsync();
                    while (response.Read())
                    {
                        roleId = response["Id"].ToString().Trim();
                    }

                    _conn.Close();
                }

                if (String.IsNullOrWhiteSpace(roleId))
                    return 2;

                await using (var cmd = new SqlCommand(command, _conn))
                {
                    cmd.Parameters.AddWithValue("@RoleId", roleId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
               
                    _conn.Open();
                    response2 = await cmd.ExecuteNonQueryAsync();

                }
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                _conn.Close();
            }
            return response2;
        }
    }
}
