using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Interfaces
{
    interface IWalletRepository
    {
        Task GetAllUserWallets(string Uid);
        Task GetIndividualUserWallet(string Uid, string walletId);
        Task DeleteWallet(string walletId);
        Task CreateWallet(string Uid);



    }
}
