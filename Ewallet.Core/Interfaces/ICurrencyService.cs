//using Ewallet.Core.DTO;
using Ewallet.Models;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface ICurrencyService
    {
        Task<string> CreateCurrency(string walletId, string code, bool isMain = false);
        Task<ResponseDTO<string>> RemoveCurrency(string currencyId);
        Task<ResponseDTO<IEnumerable<Currency>>> GetAllCurrencies(string walletId);
        Task<ResponseDTO<Currency>> GetCurrency(string currencyId);
        Task<ResponseDTO<string>> Deposit(string currencyId, decimal depositamount);
        Task<ResponseDTO<string>> Withdraw(string currencyId, decimal amount);


    }
}
