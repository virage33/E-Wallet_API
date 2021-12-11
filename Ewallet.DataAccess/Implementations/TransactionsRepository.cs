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
    public class TransactionsRepository : ITransactionRepository
    {
        private readonly IConfiguration _config;
        private readonly SqlConnection _conn;

        public TransactionsRepository(IConfiguration configuration)
        {
            _config = configuration;
            _conn = new SqlConnection(_config.GetSection("ConnectionStrings:Default").Value);

        }
        public Task DepositFunds(Transactions Deposit)
        {
            throw new NotImplementedException();
        }

        public Task SetMainCurrency(string Uid, string walletId)
        {
            throw new NotImplementedException();
        }

        public Task WithdrawFunds(Transactions Withdrawal)
        {
            throw new NotImplementedException();
        }
    }
}
