using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface ICurrencyService
    {
        Task CreateCurrency();
        Task RemoveCurrency();
        Task Deposit();
        Task Withdraw();

    }
}
