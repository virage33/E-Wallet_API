using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface ITransaction
    {
        Task GetAllTransactions();
        Task RegisterTransaction();
        Task CreditTransactions();
        Task DebitTransactions();
        Task DeleteTransaction();
        Task<bool> Deposit(string currencyId, decimal amount);
        Task<bool> Withdraw(string currencyId, string currencyID);

    }
}
