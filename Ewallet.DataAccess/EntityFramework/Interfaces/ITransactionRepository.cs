using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.EntityFramework.Interfaces
{
    public interface ITransactionRepository
    {

       
        Task<Transactions> LogTransactions(Transactions Deposit);
        Task<List<Transactions>> GetAllTransactions();
        Task<List<Transactions>> GetAllWalletTransactions(string walletid);
        Task<List<Transactions>> GetAllCurrencyTransactions(string currencyId);
        Task<Transactions> GetTransaction(string transactonId);
        Task<List<Transactions>> GetAllCreditTransactions();
        Task<List<Transactions>> GetAllWalletCreditTransactions(string walletid);
        Task<List<Transactions>> GetAllCurrencyCreditTransactions(string currencyId);
        Task<List<Transactions>> GetAllDebitTransactions();
        Task<List<Transactions>> GetAllWalletDebitTransactions(string walletid);
        Task<List<Transactions>> GetAllCurrencyDebitTransactions(string currencyId);
        Task<List<Transactions>> GetAllTransferTransactions();
        Task<List<Transactions>> GetAllWalletTransferTransactions(string walletid);
        Task<List<Transactions>> GetAllCurrencyTransferTransactions(string currencyId);

    }
}
