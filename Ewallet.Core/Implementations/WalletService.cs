using Ewallet.Commons;
using Ewallet.Core.Interfaces;
using Ewallet.DataAccess.EntityFramework.Interfaces;
//using Ewallet.DataAccess.Interfaces;
using Ewallet.Models;
using Ewallet.Models.DTO;
using Ewallet.Models.DTO.WalletDTO;
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
        

        public async Task<ResponseDTO<string>> AddCurrency(string walletId, string currencyCode, bool isMain=false)
        {
            try
            {
                var response = await _currencyService.CreateCurrency(walletId: walletId, code: currencyCode, isMain: isMain);

                if (response == "currency doesn't exist")
                    return ResponseHelper.CreateResponse<string>(message: "currency doesn't exist", data: null, status: false);
                if (response == "successful")
                    return ResponseHelper.CreateResponse<string>(message: "Successful", data: null, status: true);
            } catch(Exception e)
            {
                return ResponseHelper.CreateResponse<string>(message: "Error", data: null, status: false,error:e);
            }
            return ResponseHelper.CreateResponse<string>(message: "Something went wrong!", data: null, status: false); ;

        }

        public async Task<string> CreateWallet(string uid, string maincurrency)
        {
            WalletModel wallet = new WalletModel();
            wallet.MainCurrency = maincurrency.ToUpper().Trim();
            wallet.WalletBalance = 0;
            wallet.UserId = uid;

            var response = await _walletRepository.CreateWallet(wallet);
            if (response == 1)
            {
                var currencyResponse= await AddCurrency(walletId: wallet.Id, currencyCode: wallet.MainCurrency,isMain:true);
                if (currencyResponse.IsSuccessful == false)
                    return currencyResponse.Message;
                if (currencyResponse.IsSuccessful == true)
                    return currencyResponse.Message;
            }
                
            return null;
        }

   

        public async Task<string> DeleteWallet(string walletId)
        {
            var response = await _walletRepository.DeleteWallet(walletId);
            if (response > 0)
                return "successful";
            return null;
        }

        public async Task<List<WalletDTO>> GetAllUserWallets(string uid)
        {
            var result = new List<WalletDTO>();
            var response = await _walletRepository.GetAllUserWallets(uid);
            if (response != null)
            {
                foreach(var item in response)
                {
                    WalletDTO wallet = new WalletDTO();
                    wallet.Id = item.Id;
                    var balance = await WalletBalance(item.Id);
                    wallet.WalletBalance = balance.Data;
                    wallet.UserId = item.UserId;
                    wallet.MainCurrency = item.MainCurrency;
                    result.Add(wallet);
                }
                foreach(var item in result)
                {
                    var currencies = await _currencyService.GetAllCurrencies(item.Id);
                    foreach(var currency in currencies.Data)
                    {
                        item.Currency.Add(currency);
                    }
                }
                return result;
            }
            return null;
            
        }

        public async Task<WalletDTO> GetWallet(string walletId)
        {
            WalletDTO result = new WalletDTO();
            var response = await _walletRepository.GetIndividualUserWallet(walletId);
            if (response.UserId != null)
            {
                result.Id = response.Id;
                var balance = await WalletBalance(response.Id);
                result.WalletBalance = balance.Data;
                result.UserId = response.UserId;
                result.MainCurrency = response.MainCurrency;
                
                var currencies = await _currencyService.GetAllCurrencies(response.Id);
                foreach(var item in currencies.Data)
                {
                    result.Currency.Add(item);
                }
                return result;
            }
                    
            return null;
        }

        public async Task<bool> RemoveCurrency(string currencyId)
        {
            var response = await _currencyService.RemoveCurrency(currencyId);
            return response.IsSuccessful;
        }

        

        public async Task<ResponseDTO<decimal>> WalletBalance(string walletId)
        {
            try
            {
                decimal totalWalletBalance = 0;
                var response = await _currencyService.GetAllCurrencies(walletId);
                if (response.IsSuccessful)
                {
                    foreach(var item in response.Data)
                    {
                        totalWalletBalance += item.Balance;
                    }
                    return ResponseHelper.CreateResponse<decimal>(message: "Successful", data: totalWalletBalance, status: true);
                }

            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<decimal>(message: "Error", data: 0, status: false,error:e);
                
            }
            return ResponseHelper.CreateResponse<decimal>(message: "This wallet has no currencies", data: 0, status: false);
        }

        
    }
}
