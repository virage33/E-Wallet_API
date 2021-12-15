using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.EntityFramework.Interfaces
{
    public interface IWalletRepository
    {
        Task<List<WalletModel>> GetAllUserWallets(string Uid);
        Task<WalletModel> GetIndividualUserWallet(string walletId);
        Task<int> DeleteWallet(string walletId);
        Task<int> CreateWallet(WalletModel Uid);



    }
}
