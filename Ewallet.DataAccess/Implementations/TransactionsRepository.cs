using Ewallet.DataAccess.Interfaces;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Implementations
{
    public class TransactionsRepository : ITransactionRepository
    {
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
