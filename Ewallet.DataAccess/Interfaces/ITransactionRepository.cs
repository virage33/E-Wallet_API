using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Interfaces
{
    public interface ITransactionRepository
    {
       
        Task WithdrawFunds(Transactions Withdrawal);
        Task DepositFunds(Transactions Deposit);
    }
}
