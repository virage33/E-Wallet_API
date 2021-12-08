using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Interfaces
{
    public interface ICurrencyRepository
    {
        Task CreateCurrency(Currency data);
        Task DeleteCurrency(string Id);
        Task GetCurrency(string Id);
        Task GetAllCurrencies();
        Task DepositFunds(string currencyId, decimal amount);
        Task WithDraw(string currencyId, decimal amount);

    }
}
