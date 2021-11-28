﻿using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Interfaces
{
    interface ITransactionRepository
    {
        Task SetMainCurrency(string Uid,string walletId);
        Task WithdrawFunds(Transactions Withdrawal);
        Task DepositFunds(Transactions Deposit);
    }
}
