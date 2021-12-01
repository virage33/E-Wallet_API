using Ewallet.DataAccess.Interfaces;
using EwalletApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
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
            string isIdExist = "Select UserId from USERS WHERE UserId = @UserId";
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
                            return response = 2;
                        }   

                    }

                    //checks if userId already exists
                    await using (var cmd = new SqlCommand(isIdExist, con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@UserId", user.UserId);
                        var exist = cmd.ExecuteScalar();
                        if (exist != null)
                        {
                            return response = 3;
                        }

                    }
                    //creates new user
                    using (var cmd = new SqlCommand(command, con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@UserId", user.UserId);
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@PhoneNo", user.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Password", user.password);
                        response = (int)cmd.ExecuteNonQuery();          
                    }                       
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }

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
                    con.Close();
                    while (response.Read())
                    {
                        UserModel user = new UserModel();
                        user.UserId = response.GetString(response.GetOrdinal("UserId"));
                        user.FirstName = response.GetString(response.GetOrdinal("UserId"));
                        user.LastName = response.GetString(response.GetOrdinal("UserId"));
                        user.Email = response.GetString(response.GetOrdinal("UserId"));
                        user.password = response.GetString(response.GetOrdinal("UserId"));
                        user.PhoneNumber = int.Parse( response.GetString(response.GetOrdinal("UserId")));
                        result.Add(user);
                    }

                }
                

            
            }
            catch (Exception)
            {

                throw;
            }
           

            return result;
        }

        public UserModel GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public UserModel GetUserById(string Uid)
        {
            throw new NotImplementedException();
        }

        public UserModel GetUserByName(string username)
        {
            throw new NotImplementedException();
        }

        public UserModel GetUserByRole(string Role)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateUserProfile()
        {
            throw new NotImplementedException();
        }
    }
}
