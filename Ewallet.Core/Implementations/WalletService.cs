using Ewallet.Core.Interfaces;
using Ewallet.DataAccess.Interfaces;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Implementations
{
    public class WalletService : IWalletServices
    {
        private IWalletRepository walletRepository;
        public WalletService(IWalletRepository _walletRepository)
        {
            walletRepository = _walletRepository;
        }
        public Task ActivateMultipleWallets(string Uid)
        {
            throw new NotImplementedException();
        }

        public Task AddCurrency(string walletId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateWallet(string uid, string maincurrency)
        {
            WalletModel wallet = new WalletModel();
            wallet.MainCurrency = maincurrency;
            wallet.WalletBalance = 0;

            var response = await walletRepository.CreateWallet(wallet,uid);
            if (response == 1)
                return "successful";
            return null;
        }

        public Task<string> DeActivateMultipleWallets(string Uid)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DeleteWallet(string walletId)
        {
            var response = await walletRepository.DeleteWallet(walletId);
            if (response > 0)
                return "successful";
            return null;
        }

        public async Task<List<WalletModel>> GetAllUserWallets(string uid)
        {
            var response = await walletRepository.GetAllUserWallets(uid);
            if (response != null)
                return response;
            return null;
            
        }

        public async Task<WalletModel> GetWallet(string walletId)
        {
            var response = await walletRepository.GetIndividualUserWallet(walletId);
            if (response == null)
                return null;
            return response;
        }

        public Task RemoveCurrency(string currencyId)
        {
            throw new NotImplementedException();
        }

        public Task SetMainCurrency(string currencyId)
        {
            throw new NotImplementedException();
        }

        public Task WithdrawalAccountOperations()
        {
            throw new NotImplementedException();
        }
    }
}
