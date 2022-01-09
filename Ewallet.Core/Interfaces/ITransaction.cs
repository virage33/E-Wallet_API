using Ewallet.Models.DTO;
using Ewallet.Models.DTO.ReturnDTO;
using Ewallet.Models.DTO.WalletDTO;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface ITransaction
    {
        Task<ResponseDTO<Transactions>> LogCreditTransactions(CreditTransactionDTO data);
        Task<ResponseDTO<Transactions>> LogDebitTransactions(DebitTransactionDTO data);
        Task<ResponseDTO<Transactions>> LogTransferTransactions(TransferTransactionDTO data);
        Task DeleteTransaction();

        Task<ResponseDTO<List<TransactionReturnDTO>>> GetAllTransactions();
        Task<ResponseDTO<List<TransactionReturnDTO>>> GetAllWalletTransactions(string walletid);
        Task<ResponseDTO<List<TransactionReturnDTO>>> GetAllCurrencyTransactions(string currencyId);
        Task<ResponseDTO<object>> GetTransaction(string transactonId);
        Task<ResponseDTO<List<CreditTransactionReturnDTO>>> GetAllCreditTransactions();
        Task<ResponseDTO<List<CreditTransactionReturnDTO>>> GetAllWalletCreditTransactions(string walletid);
        Task<ResponseDTO<List<CreditTransactionReturnDTO>>> GetAllCurrencyCreditTransactions(string currencyId);
        Task<ResponseDTO<List<DebitTransactionReturnDTO>>> GetAllDebitTransactions();
        Task<ResponseDTO<List<DebitTransactionReturnDTO>>> GetAllWalletDebitTransactions(string walletid);
        Task<ResponseDTO<List<DebitTransactionReturnDTO>>> GetAllCurrencyDebitTransactions(string currencyId);
        Task<ResponseDTO<List<TransferTransactionReturnDTO>>> GetAllTransferTransactions();
        Task<ResponseDTO<List<TransferTransactionReturnDTO>>> GetAllWalletTransferTransactions(string walletid);
        Task<ResponseDTO<List<TransferTransactionReturnDTO>>> GetAllCurrencyTransferTransactions(string currencyId);

    }
}
