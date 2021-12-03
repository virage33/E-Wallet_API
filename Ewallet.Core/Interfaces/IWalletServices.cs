using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface IWalletServices
    {
        Task CreateWallet();
        Task DeleteWallet();
        Task ActivateWallet();
        Task DeActivateWallet();
        Task SetMainCurrency();
        Task WithdrawalAccountOperations();
        Task AddCurrency();
        Task RemoveCurrency();

    }
}
