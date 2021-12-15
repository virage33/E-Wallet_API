using Ewallet.DataAccess.Interfaces;
using EwalletApi.Models.AccountModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Implementations
{
    public class WalletRepository : IWalletRepository
    {
        private readonly IConfiguration _config;
        private readonly SqlConnection _conn;

        public WalletRepository(IConfiguration configuration)
        {
            _config = configuration;
            _conn = new SqlConnection(_config.GetSection("ConnectionStrings:Default").Value);

        }

        //Creates user wallets
        public async Task<int> CreateWallet(WalletModel details)
        {
           
            string command = "INSERT INTO Wallet VALUES(@WalletId,@MainCurrency,@WalletBalance,@UserId)";
            int response = 0;
            await using (var cmd = new SqlCommand(command,_conn))
            {
                cmd.Parameters.AddWithValue("@WalletId", details.Id);
                cmd.Parameters.AddWithValue("@MainCurrency", details.MainCurrency);
                cmd.Parameters.AddWithValue("@WalletBalance", details.WalletBalance);
                cmd.Parameters.AddWithValue("@UserId", details.UserId);
                _conn.Open();
                response = (int)cmd.ExecuteNonQuery();
                _conn.Close();
            }
            return response;
        }

        public async Task<int> DeleteWallet(string walletId)
        {
           
            string command = "DELETE FROM Wallet WHERE WalletId = @walletId";
            int response = 0;
            using (var cmd = new SqlCommand(command,_conn))
            {
                cmd.Parameters.AddWithValue("@walletId", walletId);
                _conn.Open();
                response = await cmd.ExecuteNonQueryAsync();
                _conn.Close();
            }
            return response;
        }

        public async Task<List<WalletModel>> GetAllUserWallets(string Uid)
        {
            List<WalletModel> result = new List<WalletModel>();
            
            string command = "SELECT * FROM Wallet WHERE UserId = @Uid";
            var cmd = new SqlCommand(command, _conn);
            try
            {     
                cmd.Parameters.AddWithValue("@Uid", Uid);
                _conn.Open();
                await using (var response=cmd.ExecuteReader())
                {
                    if (response != null)
                    {
                        while (response.Read())
                        {
                            WalletModel wallet = new WalletModel();
                            wallet.Id = response.GetString(response.GetOrdinal("WalletId")).ToString().Trim();
                            wallet.WalletBalance = response.GetDecimal(response.GetOrdinal("WalletBalance"));
                            wallet.UserId = response.GetString(response.GetOrdinal("UserId")).Trim();
                            result.Add(wallet);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _conn.Close();
            }
            
            return result;
        }

        public async Task<WalletModel> GetIndividualUserWallet(string walletId)
        {
            WalletModel result = new WalletModel();
            string command = "SELECT * FROM Wallet WHERE WalletId=@WalletId";
            var cmd = new SqlCommand(command, _conn);
            try
            {           
                cmd.Parameters.AddWithValue("@WalletId", walletId);

                _conn.Open();
                await using (var response = cmd.ExecuteReader())
                {
                    if (response != null)
                    {
                        while (response.Read())
                        {
                            result.Id = response.GetString(response.GetOrdinal("WalletId")).ToString().Trim();
                            result.WalletBalance = response.GetDecimal(response.GetOrdinal("WalletBalance"));

                        }
                    }
                }
            }
            catch (Exception )
            {

                throw;
            }
            finally
            {
                _conn.Close();
            }
            
            return result;
        }
    }
}
