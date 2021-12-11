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
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly IConfiguration _config;
        private readonly SqlConnection _conn;
        
        public CurrencyRepository(IConfiguration configuration)
        {
            _config = configuration;
            _conn = new SqlConnection(_config.GetSection("ConnectionStrings:Default").Value);
        }


        public async Task<int> CreateCurrency(Currency data)
        {
            string command = "INSERT INTO WalletCurrency Values(@Id,@WalletId,@CurrencyId,@balance,@IsMain)";
            string cmd1 = "SELECT CurrencyId FROM Currency WHERE CurrencyShortCode = @shortcode";
            string currencyId = "";
            var response2 = 0;
            try
            {

            
            using (var cmd = new SqlCommand(cmd1, _conn))
            {
                cmd.Parameters.AddWithValue("@shortcode", data.Code);
                _conn.Open();
                var response = await cmd.ExecuteReaderAsync();
                while (response.Read())
                {
                    currencyId = response["CurrencyId"].ToString();
                }
                
                _conn.Close();
            }

            if (String.IsNullOrWhiteSpace(currencyId))
                return 2;
            
                await using (var cmd = new SqlCommand(command,_conn))
            {
                cmd.Parameters.AddWithValue("@Id", data.Id);
                cmd.Parameters.AddWithValue("@CurrencyId", currencyId);
                cmd.Parameters.AddWithValue("@WalletId", data.WalletId);
                cmd.Parameters.AddWithValue("@balance", data.Balance);
                cmd.Parameters.AddWithValue("@IsMain", data.IsMain);
                _conn.Open();
                response2 = await cmd.ExecuteNonQueryAsync();
                
            }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                _conn.Close();
            }
            return response2;
        }

        public async Task<int> DeleteCurrency(string Id)
        {
            string command = "DELETE FROM WalletCurrency WHERE Id = @Id";
            var response = 0;
            try
            {
                using (var cmd = new SqlCommand(command, _conn))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    _conn.Open();
                    response = await cmd.ExecuteNonQueryAsync();
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
            
            return response;
        }

        public async Task<int> DepositOrWithdraw(string currencyId, decimal newbalance)
        {
            string command = "UPDATE WalletCurrency SET CurrencyBalance = @balance WHERE Id = @currencyId";
            var response = 0;
            try
            {
                using (var cmd = new SqlCommand(command,_conn))
                {
                    cmd.Parameters.AddWithValue("@balance",newbalance);
                    cmd.Parameters.AddWithValue("@currencyId", currencyId);
                    _conn.Open();
                    response = await cmd.ExecuteNonQueryAsync();
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
            return response;
        }

        public async Task<IEnumerable<Currency>> GetAllCurrencies(string walletId)
        {
            List<Currency> result = new List<Currency>();
            
            string command = "SELECT * FROM WalletCurrency JOIN Currency ON WalletCurrency.CurrencyId=Currency.CurrencyId WHERE WalletId = @walletId";
            try
            {

                var cmd = new SqlCommand(command, _conn);
                cmd.Parameters.AddWithValue("@walletId", walletId);
                _conn.Open();
                using (var response = await cmd.ExecuteReaderAsync())
                {
                    if (response != null)
                    {
                        while (response.Read())
                        {
                            result.Add(
                            new Currency
                            {
                                Balance = response.GetDecimal(response.GetOrdinal("CurrencyBalance")),
                                WalletId = response.GetString(response.GetOrdinal("WalletId")).Trim(),
                                IsMain = response.GetBoolean(response.GetOrdinal("IsMain")),
                                Id = response.GetString(response.GetOrdinal("Id")).Trim(),
                                Code = response.GetString(response.GetOrdinal("CurrencyShortCode")).Trim(),
                                Type = response.GetString(response.GetOrdinal("CurrencyType")).Trim()
                            }
                            );
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
            return result;
        }

        public async Task<Currency> GetCurrency(string currencyId)
        {
            Currency currency = new Currency();
            string command = "SELECT * FROM WalletCurrency JOIN Currency ON WalletCurrency.CurrencyId=Currency.CurrencyId WHERE Id = @currencyId";
            

            try
            {
                var cmd = new SqlCommand(command, _conn);
                cmd.Parameters.AddWithValue("@currencyId", currencyId);
                _conn.Open();
                using (var response = await cmd.ExecuteReaderAsync())
                {
                    if (response!=null)
                    {
                        while (response.Read())
                        {
                            currency.Balance = response.GetDecimal(response.GetOrdinal("CurrencyBalance"));
                            currency.WalletId = response.GetString(response.GetOrdinal("WalletId")).Trim();
                            currency.IsMain = response.GetBoolean(response.GetOrdinal("IsMain"));
                            currency.Id = response.GetString(response.GetOrdinal("Id")).Trim();
                            currency.Code = response.GetString(response.GetOrdinal("CurrencyShortCode")).Trim();
                            currency.Type = response.GetString(response.GetOrdinal("CurrencyType")).Trim();
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
            return currency;
        }

       
    }
}
