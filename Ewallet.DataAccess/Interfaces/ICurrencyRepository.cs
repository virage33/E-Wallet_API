using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<int> CreateCurrency(Currency data);
        Task<int> DeleteCurrency(string Id);
        Task<Currency> GetCurrency(string currencyId);
        Task<IEnumerable<Currency>> GetAllCurrencies(string walletId);
        Task<int> DepositOrWithdraw(string currencyId, decimal newBalance);
       
    }
}
