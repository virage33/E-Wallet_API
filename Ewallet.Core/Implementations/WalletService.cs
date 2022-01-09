using AutoMapper;
using Ewallet.Commons;
using Ewallet.Core.Interfaces;
using Ewallet.DataAccess.EntityFramework.Interfaces;
//using Ewallet.DataAccess.Interfaces;
using Ewallet.Models;
using Ewallet.Models.DTO;
using Ewallet.Models.DTO.WalletDTO;
using EwalletApi.Models.AccountModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IMapper _mapper;
        private readonly ICurrencyConversionService _conversionService;
        //private readonly UserManager<AppUser> _userManager;
        private IUnitOfWork _unitOfWork;


        public WalletService(IWalletRepository walletRepository, ICurrencyService currencyService, IMapper mapper, ICurrencyConversionService conversionService, IUnitOfWork unitOfWork)
        {
            _walletRepository = walletRepository;
            _currencyService = currencyService;
            this._mapper = mapper;
            this._conversionService = conversionService;
          //  this._userManager = userManager;
            _unitOfWork = unitOfWork;
        }


        public async Task<ResponseDTO<string>> AddCurrency(string walletId, string currencyCode, bool isMain=false)
        {
            try
                
            {
               
                var wallet = await _currencyService.GetAllCurrencies(walletId);
                
                if (wallet.Data.Count>0)
                {
                    foreach(var item in wallet.Data)
                    {
                        if(item.Code.ToLower() == currencyCode.ToLower())
                            return ResponseHelper.CreateResponse<string>(message: "currency already exist", data: null, status: false);
                    }
                }
                
                var response = await _currencyService.CreateCurrency(walletId: walletId, code: currencyCode, isMain: isMain);

                if (response.IsSuccessful)
                    return ResponseHelper.CreateResponse<string>(message: "Successful", data: null, status: true);
                
                return ResponseHelper.CreateResponse<string>(message: response.Message, data: null, status: false);
                
            } catch(Exception e)
            {
                return ResponseHelper.CreateResponse<string>(message: "Error", data: null, status: false,error:e);
            }

        }

        public async Task<ResponseDTO<string>> CreateWallet(string uid, string maincurrency)
        {
            try
            {
                WalletModel wallet = new WalletModel();
                wallet.MainCurrency = maincurrency.ToUpper().Trim();
                wallet.WalletBalance = 0;
                wallet.UserId = uid;

                await _unitOfWork.WalletRepository.CreateWallet(wallet);
                var response =  await _unitOfWork.Save();

                if (response >0)
                {
                    var currencyResponse = await AddCurrency(walletId: wallet.Id, currencyCode: wallet.MainCurrency, isMain: true);
                    if (currencyResponse.IsSuccessful == false)
                        return ResponseHelper.CreateResponse<string>(currencyResponse.Message, null, false);
                    return ResponseHelper.CreateResponse<string>(currencyResponse.Message, null, true);
                }

                return null;
            }catch(Exception e)
            {
                return ResponseHelper.CreateResponse<string>("error", null, false,e);
            }
        }

   
      
        public async Task<ResponseDTO<string>> DeleteWallet(string walletId)
        {
            try
            {
                //gets wallet balance
                var res = await WalletBalance(walletId);
                if (res.IsSuccessful)
                {
                    decimal walletBalance = res.Data.Balance;
                    if (walletBalance > 0)
                    {
                        return ResponseHelper.CreateResponse<string>(message: "cannot delete wallet with balance > 0", data: null, status: false);
                    }
                    var response = await _walletRepository.DeleteWallet(walletId);
                    if (response > 0)
                        return ResponseHelper.CreateResponse<string>(message: "successful", data: null, status: true);
                    return ResponseHelper.CreateResponse<string>(message: "failed", data: null, status: false);
                }
                else
                {
                    return ResponseHelper.CreateResponse<string>(message: "wallet does not exist", data: null, status: false);
                }

                
            }catch(Exception e)
            {
                return ResponseHelper.CreateResponse<string>(message: "error", data: null, status: false,e);
            }
            
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
                    wallet.WalletBalance = balance.Data.Balance;
                    wallet.UserId = item.UserId;
                    wallet.MainCurrency = balance.Data.CurrencyCode;
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
                result.WalletBalance = balance.Data.Balance;
                result.UserId = response.UserId;
                //change to main currency
                result.MainCurrency = balance.Data.CurrencyCode;
                
                var currencies = await _currencyService.GetAllCurrencies(response.Id);
                foreach(var item in currencies.Data)
                {
                    result.Currency.Add(item);
                }
                return result;
            }
                    
            return null;
        }


        public async Task<ResponseDTO<decimal>> UserBalance(string userId)
        {
            try
            {
                decimal totalBalance = 0;
                string currencyCode = "NGN";
                var res = await GetAllUserWallets(userId);
                if (res.Count > 0)
                {
                    foreach (var item in res)
                    {
                        
                        if (item.MainCurrency!= currencyCode)
                        {
                            var walletBalanceToConvert = await WalletBalance(item.Id);
                            var convertedBalance = await _conversionService.ConvertCurrency(new CurrencyConverterDTO
                            {
                                amount = walletBalanceToConvert.Data.Balance,
                                From = walletBalanceToConvert.Data.CurrencyCode,
                                To = currencyCode,
                            });
                            totalBalance += convertedBalance;
                        }
                        var walletBalance = await WalletBalance(item.Id);
                        totalBalance += walletBalance.Data.Balance;
                    }
                    return ResponseHelper.CreateResponse<decimal>(message: "successful", data: totalBalance, status: true);
                }
                return ResponseHelper.CreateResponse<decimal>(message: "failed", data: totalBalance, status: false);
            }
            catch (Exception e)
            {

                return ResponseHelper.CreateResponse<decimal>(message: "error", data: 0, status: false);
            }
        }

        //Calculates wallet balance 
        public async Task<ResponseDTO<WalletBalanceDTO>> WalletBalance(string walletId)
        {
            try
            {
                WalletBalanceDTO result = new WalletBalanceDTO();
                decimal totalWalletBalance = 0;
                var response = await _currencyService.GetAllCurrencies(walletId);
                string mainCurrency = "";
                if (response.IsSuccessful)
                {
                    foreach(var item in response.Data)
                    {
                        if (item.IsMain)
                            mainCurrency = item.Code;
                        
                    }
                    foreach(var item in response.Data)
                    {
                        if (!item.IsMain)
                        {
                            var convertertedBalance = await _conversionService.ConvertCurrency(new CurrencyConverterDTO
                            {
                                amount = item.Balance,
                                From = item.Code,
                                To = mainCurrency
                            });
                            totalWalletBalance += convertertedBalance;
                        }
                        else
                        {
                            totalWalletBalance += item.Balance;
                        }   
                    }
                    result.Balance = totalWalletBalance;
                    result.CurrencyCode = mainCurrency;
                    //updates the wallet balance on the wallet table in the db
                    await _walletRepository.UpdateWalletBalance(walletId: walletId, balance: totalWalletBalance);

                    return ResponseHelper.CreateResponse<WalletBalanceDTO>(message: "Successful", data: result, status: true);
                }
                return ResponseHelper.CreateResponse<WalletBalanceDTO>(message: "This wallet has no currencies", data: null, status: false);

            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<WalletBalanceDTO>(message: "Error", data: null, status: false,error:e);
                
            }
           
        }

        
    }
}
