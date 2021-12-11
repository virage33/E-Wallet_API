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
        private readonly IWalletRepository _walletRepository;
        private readonly ICurrencyService _currencyService;
        public WalletService(IWalletRepository walletRepository, ICurrencyService currencyService)
        {
            _walletRepository = walletRepository;
            _currencyService = currencyService;
        }
        public Task ActivateMultipleWallets(string Uid)
        {
            throw new NotImplementedException();
        }

        public async Task<string> AddCurrency(string walletId, string currencyCode, bool isMain=false)
        {
            
            var response = await _currencyService.CreateCurrency(walletId:walletId,code:currencyCode,isMain:isMain);
           
            if (response == "currency doesn't exist")
                return "currency doesn't exist";
            if (response == "successful")
                return "successful";
            return null;

        }

        public async Task<string> CreateWallet(string uid, string maincurrency)
        {
            WalletModel wallet = new WalletModel();
            wallet.MainCurrency = maincurrency.ToUpper().Trim();
            wallet.WalletBalance = 0;

            var response = await _walletRepository.CreateWallet(wallet,uid);
            if (response == 1)
            {
                var currencyResponse= await AddCurrency(walletId: wallet.Id, currencyCode: wallet.MainCurrency,isMain:true);
                if (currencyResponse == "currency doesn't exist")
                    return "currency doesn't exist";
                if (currencyResponse == "successful")
                    return "successful";
            }
                
            return null;
        }

        public Task<string> DeActivateMultipleWallets(string Uid)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DeleteWallet(string walletId)
        {
            var response = await _walletRepository.DeleteWallet(walletId);
            if (response > 0)
                return "successful";
            return null;
        }

        public async Task<List<WalletModel>> GetAllUserWallets(string uid)
        {
            var response = await _walletRepository.GetAllUserWallets(uid);
            if (response != null)
            {
                foreach(var item in response)
                {
                    var currencies = await _currencyService.GetAllCurrencies(item.Id);
                    foreach(var currency in currencies.Data)
                    {
                        item.Currency.Add(currency);
                    }
                }
                return response;
            }
            return null;
            
        }

        public async Task<WalletModel> GetWallet(string walletId)
        {
            var response = await _walletRepository.GetIndividualUserWallet(walletId);
            if (response != null)
            {
                var currencies = await _currencyService.GetAllCurrencies(response.Id);
                foreach(var item in currencies.Data)
                {
                    response.Currency.Add(item);
                }
                return response;
            }
                    
            return null;
        }

        public async Task<bool> RemoveCurrency(string currencyId)
        {
            var response = await _currencyService.RemoveCurrency(currencyId);
            return response.IsSuccessful;
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
