//using Ewallet.Core.DTO;
using Ewallet.Models;
using Ewallet.Models.DTO;
using Ewallet.Models.DTO.InputDTO;
using Ewallet.Models.DTO.WalletDTO;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface ICurrencyService
    {
        Task<ResponseDTO<string>> CreateCurrency(string walletId, string code, bool isMain = false);
        Task<ResponseDTO<string>> RemoveCurrency(string currencyId);
        Task<ResponseDTO<List<CurrencyDTO>>> GetAllCurrencies(string walletId);
        Task<ResponseDTO<CurrencyDTO>> GetCurrency(string currencyId);
        Task<ResponseDTO<string>> Deposit(string currencyId, decimal depositamount);
        Task<ResponseDTO<string>> Withdraw(DebitDTO details);

        Task<ResponseDTO<string>> Transfer(TransferFundsDTO details);


    }
}
