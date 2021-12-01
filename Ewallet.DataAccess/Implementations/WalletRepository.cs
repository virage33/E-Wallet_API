using Ewallet.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.Implementations
{
    public class WalletRepository : IWalletRepository
    {
        public Task CreateWallet(string Uid)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWallet(string walletId)
        {
            throw new NotImplementedException();
        }

        public Task GetAllUserWallets(string Uid)
        {
            throw new NotImplementedException();
        }

        public Task GetIndividualUserWallet(string Uid, string walletId)
        {
            throw new NotImplementedException();
        }
    }
}
