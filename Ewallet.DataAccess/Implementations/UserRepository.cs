using Ewallet.DataAccess.Interfaces;
using EwalletApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {

        private string CnString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\USERS\\HP\\SOURCE\\REPOS\\EWALLETAPI\\EWALLET.DB\\DB.MDF;Integrated Security = True; Connect Timeout = 30";


        public async Task<int> CreateUser(UserModel user)
        {
            string command = "INSERT INTO Users Values(@UserId,@FirstName,@LastName,@Email,@Password,@PhoneNumber)";
            string isEmailExist = "Select Email from USERS WHERE Email = @Email";
            int response = 0;

            try
            {
                await using (var con = new SqlConnection(CnString))
                {
                    //checks if email already exists
                    await using (var cmd= new SqlCommand(isEmailExist,con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        var exist = cmd.ExecuteScalar();
                        if (exist != null)
                        {
                            con.Close();
                            return response = 2;
                        }
                        con.Close();
                    }

                    
                    //creates new user
                    using (var cmd = new SqlCommand(command, con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@UserId", user.UserId);
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Password", user.password);
                        response = (int)cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    
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
                var con = new SqlConnection(CnString);
                
                await using (var cmd = new SqlCommand(command, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@UserId", Uid);
                    response = (int)cmd.ExecuteNonQuery();
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
                var con = new SqlConnection(CnString);

                var cmd = new SqlCommand(command, con);
                    
                con.Open();

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
                con.Close();


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
            UserModel user = new UserModel();

            try
            {
                var con = new SqlConnection(CnString);

                var cmd = new SqlCommand(command, con);
                cmd.Parameters.AddWithValue("@Email", email);
                con.Open();

                await using (var response = cmd.ExecuteReader())
                {
                  
                    while (response.Read())
                    {

                       // user.UserId = response.GetString(response.GetOrdinal("UserId"));
                        user.FirstName = response.GetString(response.GetOrdinal("FirstName")).Trim();
                        user.LastName = response.GetString(response.GetOrdinal("LastName")).Trim();
                        user.Email = response.GetString(response.GetOrdinal("Email")).Trim();
                        user.password = response.GetString(response.GetOrdinal("Password")).Trim();
                        user.PhoneNumber = response.GetString(response.GetOrdinal("PhoneNumber")).Trim();

                    }

                }

                con.Close();

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
                var con = new SqlConnection(CnString);

                var cmd = new SqlCommand(command, con);
                cmd.Parameters.AddWithValue("@UserId", Uid);
                con.Open();

                await using (var response = cmd.ExecuteReader())
                {
                   
                    while (response.Read())
                    {

                        //user.UserId = response.GetString(response.GetOrdinal("UserId"));
                        user.FirstName = response.GetString(response.GetOrdinal("FirstName")).Trim();
                        user.LastName = response.GetString(response.GetOrdinal("LastName")).Trim();
                        user.Email = response.GetString(response.GetOrdinal("Email")).Trim();
                        user.password = response.GetString(response.GetOrdinal("Password")).Trim();
                        user.PhoneNumber = response.GetString(response.GetOrdinal("PhoneNumber")).Trim();

                    }

                }
                con.Close();


            }
            catch (Exception)
            {

                throw;
            }
            return user;
        }

        public async Task<List<UserModel>> GetUserByName(string username)
        {
            string command = "SELECT * FROM Users WHERE FirstName LIKE @Name OR LastName LIKE @Name";
            List<UserModel> result = new List<UserModel>();

            try
            {
                var con = new SqlConnection(CnString);

                var cmd = new SqlCommand(command, con);
                cmd.Parameters.AddWithValue("@Name", username+"%");
                con.Open();

                await using (var response = cmd.ExecuteReader())
                {
                 
                    while (response.Read())
                    {
                        UserModel user = new UserModel();
                        //user.UserId = response.GetString(response.GetOrdinal("UserId"));
                        user.FirstName = response.GetString(response.GetOrdinal("FirstName")).Trim();
                        user.LastName = response.GetString(response.GetOrdinal("LastName")).Trim();
                        user.Email = response.GetString(response.GetOrdinal("Email")).Trim();
                        user.password = response.GetString(response.GetOrdinal("Password")).Trim();
                        user.PhoneNumber = response.GetString(response.GetOrdinal("PhoneNumber")).Trim();
                        result.Add(user);
                    }

                }
                con.Close();


            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public Task<List<UserModel>> GetUsersByRole(string Role)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateUserProfile(UserModel user)
        {
            throw new NotImplementedException();
        }

       
    }
}
