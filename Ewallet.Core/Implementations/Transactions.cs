using Ewallet.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Implementations
{
    class Transactions : ITransaction
    {
        public Task CreditTransactions()
        {
            throw new NotImplementedException();
        }

        public Task DebitTransactions()
        {
            throw new NotImplementedException();
        }

        public Task DeleteTransaction()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Deposit(string currencyId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public Task GetAllTransactions()
        {
            throw new NotImplementedException();
        }

        public Task RegisterTransaction()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Withdraw(string currencyId, string currencyID)
        {
            throw new NotImplementedException();
        }
    }
}
