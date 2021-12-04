using Ewallet.DataAccess.Interfaces;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Implementations
{
    public class WalletRepository : IWalletRepository
    {
        private string CnString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\USERS\\HP\\SOURCE\\REPOS\\EWALLETAPI\\EWALLET.DB\\DB.MDF;Integrated Security = True; Connect Timeout = 30";
        

        //Creates user wallets
        public async Task<int> CreateWallet(WalletModel details)
        {
            var con = new SqlConnection(CnString);
            string command = "INSERT INTO Wallet VALUES(@WalletId,@MainCurrency,@WalletBalance,@UserId)";
            int response = 0;
            await using (var cmd = new SqlCommand(command,con))
            {
                cmd.Parameters.AddWithValue("@WalletId", details.Id);
                cmd.Parameters.AddWithValue("@MainCurrency", details.MainCurrency);
                cmd.Parameters.AddWithValue("@WalletBalance", details.WalletBalance);
                cmd.Parameters.AddWithValue("@UserId", details.User.UserId);
                con.Open();
                response = (int)cmd.ExecuteNonQuery();
                con.Close();
            }
            return response;
        }

        public async Task<int> DeleteWallet(string walletId)
        {
            var con = new SqlConnection(CnString);
            string command = "DELETE * FROM Wallet WHERE WalletId = @walletId";
            int response = 0;
            using (var cmd = new SqlCommand(command,con))
            {
                cmd.Parameters.AddWithValue("@walletId", walletId);
                con.Open();
                response = cmd.ExecuteNonQuery();
                con.Close();
            }
            return response;
        }

        public async Task<List<WalletModel>> GetAllUserWallets(string Uid)
        {
            List<WalletModel> result = new List<WalletModel>();
            var con = new SqlConnection(CnString);
            string command = "SELECT * FROM Wallet WHERE UserId = @Uid";
            var cmd = new SqlCommand(command, con);
            cmd.Parameters.AddWithValue("@Uid", Uid);
            con.Open();
            await using (var response=cmd.ExecuteReader())
            {
                while (response.Read())
                {
                    WalletModel wallet = new WalletModel();
                    wallet.Id = response.GetGuid(response.GetOrdinal("WalletId")).ToString().Trim();
                    wallet.WalletBalance = Convert.ToDecimal(response.GetString(response.GetOrdinal("WalletBalance")).Trim());
                    wallet.User.UserId = response.GetGuid(response.GetOrdinal("UserId")).ToString().Trim();
                    result.Add(wallet);
                }
            }
            con.Close();
            return result;
        }

        public async Task<WalletModel> GetIndividualUserWallet(string Uid, string walletId)
        {
            WalletModel result = new WalletModel();
            var con = new SqlConnection(CnString);
            string command = "SELECT * FROM Wallet WHERE UserId = @Uid AND WalletId=@WalletId";
            var cmd = new SqlCommand(command, con);
            cmd.Parameters.AddWithValue("@Uid", Uid);
            cmd.Parameters.AddWithValue("@WalletId", walletId);

            con.Open();
            await using (var response = cmd.ExecuteReader())
            {
                while (response.Read())
                {             
                    result.Id = response.GetGuid(response.GetOrdinal("WalletId")).ToString().Trim();
                    result.WalletBalance = Convert.ToDecimal(response.GetString(response.GetOrdinal("WalletBalance")).Trim());
                    result.User.UserId = response.GetGuid(response.GetOrdinal("UserId")).ToString().Trim();
                }
            }
            con.Close();
            return result;
        }
    }
}
