using Ewallet.Models.AccountModels;
using Ewallet.Models.DTO;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<int> CreateCurrency(WalletCurrency data,string code);
        Task<int> DeleteCurrency(string Id);
        Task<CurrencyDTO> GetCurrency(string currencyId);
        Task<IEnumerable<CurrencyDTO>> GetAllCurrencies(string walletId);
        Task<int> DepositOrWithdraw(string currencyId, decimal newBalance);
       
    }
}
