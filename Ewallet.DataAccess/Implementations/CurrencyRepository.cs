using Ewallet.DataAccess.Interfaces;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Implementations
{
    public class CurrencyRepository : ICurrencyRepository
    {

        private string CnString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\USERS\\HP\\SOURCE\\REPOS\\EWALLETAPI\\EWALLET.DB\\DB.MDF;Integrated Security = True; Connect Timeout = 30";
        
        public Task CreateCurrency(Currency data)
        {
            string command = "INSERT INTO Currency Values(@CurrencyId, @CurrencyType, @ShortCode,@balance,@WalletId)";
            var con = new SqlConnection(CnString);
            using (var cmd = new SqlCommand(command,con))
            {
                cmd.Parameters.AddWithValue("@CurrencyId", data.Id);
                cmd.Parameters.AddWithValue("@CurrencyType", data.Type);
                cmd.Parameters.AddWithValue("@ShortCode", data.Code);
            }
            return Task.CompletedTask;
        }

        public Task DeleteCurrency(string Id)
        {
            throw new NotImplementedException();
        }

        public Task DepositFunds(string currencyId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public Task GetAllCurrencies()
        {
            throw new NotImplementedException();
        }

        public Task GetCurrency(string Id)
        {
            throw new NotImplementedException();
        }

        public Task WithDraw(string currencyId, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
