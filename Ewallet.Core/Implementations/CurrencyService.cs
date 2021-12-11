using Ewallet.Commons;
//using Ewallet.Core.DTO;
using Ewallet.Core.Interfaces;
using Ewallet.DataAccess.Interfaces;
using Ewallet.Models;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }



        public async Task<string> CreateCurrency(string walletId,string code, bool isMain=false)
        {
            Currency currency = new Currency();
            currency.WalletId = walletId;
            currency.IsMain = isMain;
            currency.Code = code.ToUpper();
            currency.Balance = 0;

            var response = await _currencyRepository.CreateCurrency(currency);
            if (response == 2)
                return "currency doesn't exist";
            if (response == 1)
                return "successful";
            return null;
        }

        public async Task<ResponseDTO<string>> Deposit(string currencyId,decimal depositamount)
        {
            decimal currentBalance = 0;
            var res = new ResponseDTO<string>();
            try
            {
                var response1 = await GetCurrency(currencyId);

                if (response1.IsSuccessful == false)
                    return ResponseHelper.CreateResponse<string>(message: "Currency address doesn't exist!", data: null, status: false);

                currentBalance = response1.Data.Balance;

                decimal newBalance = currentBalance + depositamount;

                var response = await _currencyRepository.DepositOrWithdraw(currencyId: currencyId, newBalance: newBalance);
                if (response > 0)
                    return ResponseHelper.CreateResponse<string>(message: "Transaction Successful", data: null, status: true);

            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<string>(message: "Error", data: null, status: false, error:e);
            }
            return ResponseHelper.CreateResponse<string>(message: "Transaction unsuccessful", data: null, status: false);
        }

        //gets all currencies in a wallet
        public async Task<ResponseDTO<IEnumerable<Currency>>> GetAllCurrencies(string walletId)
        {
            var res = new ResponseDTO<IEnumerable<Currency>>();
            try
            {
                var response = await _currencyRepository.GetAllCurrencies(walletId);
                if (response != null)
                    return res = ResponseHelper.CreateResponse<IEnumerable<Currency>>(message: "Successful", data: response, status: true);
            }
            catch(Exception e)
            {
                res = ResponseHelper.CreateResponse<IEnumerable<Currency>>(message: "Error", data: null, status: false,error:e);
                return res;
            }
            res = ResponseHelper.CreateResponse<IEnumerable<Currency>>(message: "Unsuccessful", data: null, status: true);
            return res;
        }


        //gets a single currency 
        public async Task<ResponseDTO<Currency>> GetCurrency(string currencyId)
        {
            var res = new ResponseDTO<Currency>();
            try
            {
                var response = await _currencyRepository.GetCurrency(currencyId);
                if (response.WalletId != null)
                {
                    res = ResponseHelper.CreateResponse<Currency>(message: "Successful", data: response, status: true);
                    return res;
                }
            }
            catch (Exception e)
            {
                res = ResponseHelper.CreateResponse<Currency>(message: "Error", data: null, status: false, error:e);
                return res;
            }

            res = ResponseHelper.CreateResponse<Currency>(message: "Currency Address doesn't exist!", data: null, status: false);
            return res;
        }



        // Deletes currency from wallet
        public async Task<ResponseDTO<string>> RemoveCurrency(string currencyId)
        {
            var res = new ResponseDTO<string>();
            try
            {
                var response = await _currencyRepository.DeleteCurrency(currencyId);
                if (response > 0)
                {
                    res = ResponseHelper.CreateResponse<string>(message: "Successful", data: null, status: true);
                    return res;
                }
            }catch (Exception e)
            {
                return ResponseHelper.CreateResponse<string>(message: "Error", data: null, status: false,error:e);
            }
            res = ResponseHelper.CreateResponse<string>(message:"Unsuccessful",data:null,status:false);
                
            return res;
        }


        //withdraws from currency balance
        public async Task<ResponseDTO<string>> Withdraw(string currencyId, decimal withdrawalAmount)
        {
            decimal currentBalance = 0;
            var res = new ResponseDTO<string>();

            try
            {
                var response1 = await GetCurrency(currencyId);

                if (response1.IsSuccessful == false)
                {
                    res = ResponseHelper.CreateResponse<string>(message: "Currency address doesn't exist", data: null, status: false);
                    return res;
                }

                currentBalance = response1.Data.Balance;

                if (currentBalance < withdrawalAmount)
                {
                    res = ResponseHelper.CreateResponse<string>(message: "Insufficient Funds!", data: null, status: false);

                    return res;
                }


                decimal newBalance = currentBalance - withdrawalAmount;

                var response = await _currencyRepository.DepositOrWithdraw(currencyId: currencyId, newBalance: newBalance);
                if (response > 0)
                {
                    res = ResponseHelper.CreateResponse<string>(message: "Transaction Successful", data: null, status: true);
                    return res;
                }
            }
            catch (Exception e)
            {
                return res = ResponseHelper.CreateResponse<string>(message: "Error", data: null, status: true,error:e);
            }

            res = ResponseHelper.CreateResponse<string>(message: "Something went wrong. Transaction Unsuccessful", data: null, status: false);
           
            return res;


        }
    }
}
