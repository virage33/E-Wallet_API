using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface IWalletServices
    {
        Task<string> CreateWallet(string uid, string maincurrency);
        Task<string> DeleteWallet(string walletId);
        Task ActivateMultipleWallets(string walletId);
        Task<string> DeActivateMultipleWallets(string walletId);
        Task SetMainCurrency(string currencyId);
        Task WithdrawalAccountOperations();
        Task<string> AddCurrency(string walletId, string currencyCode, bool isMain = false);
        Task<bool> RemoveCurrency(string currencyId);
        Task<List<WalletModel>> GetAllUserWallets(string uid);
        Task<WalletModel> GetWallet(string walletId);

    }
}
