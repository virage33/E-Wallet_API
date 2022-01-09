using Ewallet.Commons;
//using Ewallet.Core.DTO;
using Ewallet.Core.Interfaces;
using Ewallet.DataAccess.EntityFramework.Interfaces;
//using Ewallet.DataAccess.Interfaces;
using Ewallet.Models;
using Ewallet.Models.AccountModels;
using Ewallet.Models.DTO;
using Ewallet.Models.DTO.InputDTO;
using Ewallet.Models.DTO.WalletDTO;
using EwalletApi.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ITransaction _transactionService;
        private readonly ICurrencyConversionService _currencyConversionService;
        private readonly UserManager<AppUser> _userManager;

        public CurrencyService(ICurrencyRepository currencyRepository, ITransaction transactionService, ICurrencyConversionService currencyConversionService, UserManager<AppUser> userManager)
        {
            _currencyRepository = currencyRepository;
            this._transactionService = transactionService;
            this._currencyConversionService = currencyConversionService;
            this._userManager = userManager;
        }



        public async Task<ResponseDTO<string>> CreateCurrency(string walletId,string code, bool isMain=false)
        {
            try
            {
                WalletCurrency currency = new WalletCurrency();
                currency.WalletId = walletId;
                currency.IsMain = isMain;

                currency.Currencybalance = 0;

                var response = await _currencyRepository.CreateCurrency(currency, code.ToUpper());
                if (response == 2)
                    return ResponseHelper.CreateResponse<string>(message: "currency doesn't exist", data: null,status: false);
                if (response == 1)
                    return ResponseHelper.CreateResponse<string>(message: "successful", data: null, status: true);
                return ResponseHelper.CreateResponse<string>(message: "failed", data: null, status: false); ;
            }
            catch (Exception e)
            {

                return ResponseHelper.CreateResponse<string>(message: "error!", data: null, status: false,error:e);
            }
        }





        //deposit funds in wallet
        public async Task<ResponseDTO<string>> Deposit(string currencyId,decimal depositamount)
        {
            decimal currentBalance = 0;
            var res = new ResponseDTO<string>();
            var transactonRecordObject = new CreditTransactionDTO();
            
            try
            {
                //gets the currency to be credited
                var destinationCurrency = await GetCurrency(currencyId);

                //returns message if currency address not found
                if (destinationCurrency.IsSuccessful == false)
                    return ResponseHelper.CreateResponse<string>(message: "Currency address doesn't exist!", data: null, status: false);

                //gets the current balance of the currency to be credit
                currentBalance = destinationCurrency.Data.Balance;

                //adds deposit amount to current balance
                decimal newBalance = currentBalance + depositamount;

                //updates the balance of the currency in the database
                var response = await _currencyRepository.DepositOrWithdraw(currencyId: currencyId, newBalance: newBalance);
                if (response > 0)
                {
                    //transaction record dto object
                    //logs successful transactions
                    transactonRecordObject.Amount = depositamount;
                    transactonRecordObject.CurrencyShortCode = destinationCurrency.Data.Code;
                    transactonRecordObject.DestinationCurrencyAddress = currencyId;
                    transactonRecordObject.WalletCurrencyId = currencyId;
                    transactonRecordObject.DestinationWalletAddress = destinationCurrency.Data.WalletId;
                    transactonRecordObject.Remark = "successful";
                    transactonRecordObject.TransactionType = "credit";

                    //logs successful transaction in the database
                    var successfulTransactionHistory = await _transactionService.LogCreditTransactions(transactonRecordObject);
                    return ResponseHelper.CreateResponse<string>(message: "Transaction Successful", data: null, status: true);
                }

                //logs unsuccessful transaction in the database
                transactonRecordObject.Amount = depositamount;
                transactonRecordObject.CurrencyShortCode = destinationCurrency.Data.Code;
                transactonRecordObject.DestinationCurrencyAddress = currencyId;
                transactonRecordObject.DestinationWalletAddress = destinationCurrency.Data.WalletId;
                transactonRecordObject.Remark = "failed";
                transactonRecordObject.TransactionType = "credit";
                
                var failedTransactionHistory = await _transactionService.LogCreditTransactions(transactonRecordObject);
                
                return ResponseHelper.CreateResponse<string>(message: "Transaction unsuccessful", data: null, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<string>(message: "Error", data: null, status: false, error:e);
            }

        }

        


        //gets all currencies in a wallet
        public async Task<ResponseDTO<List<CurrencyDTO>>> GetAllCurrencies(string walletId)
        {
            var res = new ResponseDTO<List<CurrencyDTO>>();
            try
            {
                var response = await _currencyRepository.GetAllCurrencies(walletId);
                if (response != null)
                    return res = ResponseHelper.CreateResponse<List<CurrencyDTO>>(message: "Successful", data: response, status: true);
            }
            catch(Exception e)
            {
                res = ResponseHelper.CreateResponse<List<CurrencyDTO>>(message: "Error", data: null, status: false,error:e);
                return res;
            }
            res = ResponseHelper.CreateResponse<List<CurrencyDTO>>(message: "Unsuccessful", data: null, status: true);
            return res;
        }






        //gets a single currency 
        public async Task<ResponseDTO<CurrencyDTO>> GetCurrency(string currencyId)
        {
            var res = new ResponseDTO<CurrencyDTO>();
            try
            {
                var response = await _currencyRepository.GetCurrency(currencyId);
                if (response.WalletId != null)
                {
                    res = ResponseHelper.CreateResponse<CurrencyDTO>(message: "Successful", data: response, status: true);
                    return res;
                }
            }
            catch (Exception e)
            {
                res = ResponseHelper.CreateResponse<CurrencyDTO>(message: "Error", data: null, status: false, error:e);
                return res;
            }

            res = ResponseHelper.CreateResponse<CurrencyDTO>(message: "Currency Address doesn't exist!", data: null, status: false);
            return res;
        }





        // Deletes currency from wallet
        public async Task<ResponseDTO<string>> RemoveCurrency(string currencyId)
        {
            var res = new ResponseDTO<string>();
            try
            {
                var currency = await GetCurrency(currencyId);
                if (currency.IsSuccessful)
                {
                    if (currency.Data.IsMain)
                        return ResponseHelper.CreateResponse<string>(message: "cannot delete main currency", data: null, status: false);
                }
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


        //this transfer funds from one currency to another within the system 
        public async Task<ResponseDTO<string>> Transfer(TransferFundsDTO details)
        {
            var debitTransaction = new DebitTransactionDTO();
            var creditTransaction = new CreditTransactionDTO();
            var transferTransaction = new TransferTransactionDTO();
            try
            {
                //gets the sender and beneficiary currency objects 
                var senderCurrency = await GetCurrency(details.SenderCurrencyAddress);
                var beneficiaryCurrency = await GetCurrency(details.BeneficiaryCurrencyAddressId);

                //returns if sender address doesn't exist
                if (!senderCurrency.IsSuccessful)
                    return ResponseHelper.CreateResponse<string>(message: "address doesn't exist!", data: null, status: false);
                //returns if beneficiary address doesn't exist
                if (!beneficiaryCurrency.IsSuccessful)
                    return ResponseHelper.CreateResponse<string>(message: "address doesn't exist!", data: null, status: false);

                //Current balance of the sender and the beneficiary currency
                var senderCurrentBalance = senderCurrency.Data.Balance;
                var beneficiaryCurrentBalance = beneficiaryCurrency.Data.Balance;

                if (senderCurrentBalance > details.Amount)
                {
                    //updates the sender currency balance
                    var newSenderBalance = senderCurrentBalance - details.Amount;
                    var res = await _currencyRepository.DepositOrWithdraw(currencyId: senderCurrency.Data.Id, newBalance: newSenderBalance);
                    if (res > 0)
                    {
                        //logs successful debit transaction from sender wallet currency
                        debitTransaction.Amount = details.Amount;
                        debitTransaction.BeneficiaryAddress = details.BeneficiaryCurrencyAddressId;
                        debitTransaction.BeneficiaryName = details.BeneficiaryName;
                        debitTransaction.CurrencyShortCode = senderCurrency.Data.Code;
                        debitTransaction.FinancialInstitutionType = "Ewallet";
                        debitTransaction.InstitutionName = "QWallet";
                        debitTransaction.Remark = "successful";
                        debitTransaction.TransactionType = "debit";
                        debitTransaction.WalletId = senderCurrency.Data.WalletId;
                        debitTransaction.WalletCurrencyId = senderCurrency.Data.Id;

                        await _transactionService.LogDebitTransactions(debitTransaction);

                        var amount = details.Amount;

                        //converts the transfer amount to the beneficiary address currency
                        if(beneficiaryCurrency.Data.Code != senderCurrency.Data.Code)
                        {
                            amount = await _currencyConversionService.ConvertCurrency(new CurrencyConverterDTO { 
                                amount=details.Amount,
                                From = senderCurrency.Data.Code,
                                To = beneficiaryCurrency.Data.Code,
                            });
                        }

                        //updates the beneficiary currency balance the new balance
                        var newBeneficiaryBalance = beneficiaryCurrentBalance + amount;
                        res = await _currencyRepository.DepositOrWithdraw(currencyId: beneficiaryCurrency.Data.Id, newBalance: newBeneficiaryBalance);
                        if (res > 0)
                        {
                            creditTransaction.Amount = amount;
                            creditTransaction.CurrencyShortCode = beneficiaryCurrency.Data.Code;
                            creditTransaction.DestinationCurrencyAddress = beneficiaryCurrency.Data.Id;
                            creditTransaction.WalletCurrencyId = beneficiaryCurrency.Data.WalletId;
                            creditTransaction.DestinationWalletAddress = beneficiaryCurrency.Data.WalletId;
                            creditTransaction.Remark = "successful";
                            creditTransaction.TransactionType = "credit";

                            //logs successful transaction in the database
                            await _transactionService.LogCreditTransactions(creditTransaction);
                            return ResponseHelper.CreateResponse<string>(message: "Transaction Successful", data: null, status: true);
                        }

                        //reverses the transaction by reverting the sender balance to its original balance if the beneficiary balance could not be updated 
                        res = await _currencyRepository.DepositOrWithdraw(currencyId: senderCurrency.Data.Id, newBalance: senderCurrentBalance);

                        creditTransaction.Amount = details.Amount;
                        creditTransaction.CurrencyShortCode = senderCurrency.Data.Code;
                        creditTransaction.DestinationCurrencyAddress = senderCurrency.Data.Id;
                        creditTransaction.WalletCurrencyId = senderCurrency.Data.WalletId;
                        creditTransaction.DestinationWalletAddress = senderCurrency.Data.WalletId;
                        creditTransaction.Remark = "successful";
                        creditTransaction.TransactionType = "credit";

                        //logs successful transaction in the database
                        await _transactionService.LogCreditTransactions(creditTransaction);
                        return ResponseHelper.CreateResponse<string>(message: "Transaction Failed", data: null, status:false);    
                    }

                    transferTransaction.Amount = details.Amount;
                    transferTransaction.BeneficiaryName = details.BeneficiaryName;
                    transferTransaction.BeneficiaryWalletAddress = details.BeneficiaryCurrencyAddressId;
                    transferTransaction.CurrencyShortCode = senderCurrency.Data.Code;
                    transferTransaction.TransactionType = "transfer";
                    transferTransaction.WalletCurrencyId = details.SenderCurrencyAddress;
                    transferTransaction.SenderWalletAddress = senderCurrency.Data.WalletId;
                    transferTransaction.WalletId = senderCurrency.Data.WalletId;
                    transferTransaction.Remark = "failed";

                    await _transactionService.LogTransferTransactions(transferTransaction);
                    //returns if sender balance could not be updated
                    return ResponseHelper.CreateResponse<string>(message: "Transaction Failed", data: null, status: false);
                }

                transferTransaction.Amount = details.Amount;
                transferTransaction.BeneficiaryName = details.BeneficiaryName;
                transferTransaction.BeneficiaryWalletAddress = details.BeneficiaryCurrencyAddressId;
                transferTransaction.CurrencyShortCode = senderCurrency.Data.Code;
                transferTransaction.TransactionType = "transfer";
                transferTransaction.WalletCurrencyId = details.SenderCurrencyAddress;
                transferTransaction.SenderWalletAddress = senderCurrency.Data.WalletId;
                transferTransaction.WalletId = senderCurrency.Data.WalletId;
                transferTransaction.Remark = "failed";

                await _transactionService.LogTransferTransactions(transferTransaction);
                //returns if sender current balance is less than transfer amount
                return ResponseHelper.CreateResponse<string>(message: "Insufficient Funds", data: null, status: false);
                

            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<string>(message: "error", data: null, status: false, e);
            }
        }






        //withdraws from currency balance
        public async Task<ResponseDTO<string>> Withdraw(DebitDTO details )
        {
            decimal currentBalance = 0;
            var res = new ResponseDTO<string>();
            var transactionRecordObject = new DebitTransactionDTO();
            decimal newBalance = 0;

            try
            {
                //gets the currency to be withdrawn from
                var response1 = await GetCurrency(details.CurrencyId);
                
                if (response1.IsSuccessful == false)
                {
                    //returns false if currency is not found
                    return  ResponseHelper.CreateResponse<string>(message: "Currency address doesn't exist", data: null, status: false);
                }
                var currencyShortCode = response1.Data.Code;
                currentBalance = response1.Data.Balance;

                //checks if the current balance is less than the amount to be withdrawn
                if (currentBalance < details.Amount)
                {
                    //if (isTransferable != false)
                    //{
                    List<string> affectedCurrencies = new List<string>();                   
                    
                    //gets all currencies in the present wallet
                    var walletCurrencies = await GetAllCurrencies(response1.Data.WalletId);

                    
                    if(walletCurrencies.IsSuccessful != false)
                    {
                        //loops through each currency to get their balance
                        foreach(var item in walletCurrencies.Data)
                        {
                            //adds currency address to the list
                            affectedCurrencies.Add(item.Id);

                            //converts the currency balance to the currency that is to be withdrawn
                            CurrencyConverterDTO conversionDetails = new CurrencyConverterDTO {
                                amount = item.Balance,
                                From = item.Code,
                                To = currencyShortCode
                            };
                            var converted = await _currencyConversionService.ConvertCurrency(conversionDetails);
                           
                            //adds it to the current balance
                            currentBalance += converted;

                            //checks if the balance is enough for withdrawal
                            if (currentBalance >= details.Amount)
                            {
                                //deducts withdrawal amount from current balance
                                newBalance = currentBalance - details.Amount;

                                //loops through all the currency addresses that were added to teh list and sets their balance to 0
                                foreach (string wca in affectedCurrencies)
                                {
                                    await _currencyRepository.DepositOrWithdraw(currencyId: wca, newBalance: 0);
                                }

                                //logs successful transactions
                                transactionRecordObject.Amount = details.Amount;
                                transactionRecordObject.BeneficiaryAddress = details.BeneficiaryAddress;
                                transactionRecordObject.BeneficiaryName = details.BeneficiaryName;
                                transactionRecordObject.CurrencyShortCode = response1.Data.Code;
                                transactionRecordObject.FinancialInstitutionType = details.FinancialInstitutionType;
                                transactionRecordObject.InstitutionName = details.InstitutionName;
                                transactionRecordObject.Remark = "successful";
                                transactionRecordObject.TransactionType = "debit";
                                transactionRecordObject.WalletId = response1.Data.WalletId;
                                transactionRecordObject.WalletCurrencyId = response1.Data.Id;

                                await _transactionService.LogDebitTransactions(transactionRecordObject);

                                //sets the balance of the original withdrawal currency to 0
                                await _currencyRepository.DepositOrWithdraw(currencyId: details.CurrencyId, newBalance: 0);
                                
                                //converts the remaining balance of the transaction back to the currency of the last currency that was added to teh list
                                conversionDetails = new CurrencyConverterDTO
                                {
                                    amount = newBalance,
                                    From = currencyShortCode,
                                    To = item.Code,
                                };
                                var reverse = await _currencyConversionService.ConvertCurrency(conversionDetails);
                                
                                //updates the balance of the last currency added to the list to the new balance
                                await _currencyRepository.DepositOrWithdraw(currencyId: item.Id, newBalance: reverse);
                                return ResponseHelper.CreateResponse<string>(message: "Transaction Successful", data: null, status: true);
                            }
                        }
                    }
                    //}

                    //logs failed transactions
                    transactionRecordObject.Amount = details.Amount;
                    transactionRecordObject.BeneficiaryAddress = details.BeneficiaryAddress;
                    transactionRecordObject.BeneficiaryName = details.BeneficiaryName;
                    transactionRecordObject.CurrencyShortCode = response1.Data.Code;
                    transactionRecordObject.FinancialInstitutionType = details.FinancialInstitutionType;
                    transactionRecordObject.InstitutionName = details.InstitutionName;
                    transactionRecordObject.Remark = "failed";
                    transactionRecordObject.TransactionType = "debit";
                    transactionRecordObject.WalletId = response1.Data.WalletId;
                    transactionRecordObject.WalletCurrencyId = response1.Data.Id;

                    await _transactionService.LogDebitTransactions(transactionRecordObject);

                    return ResponseHelper.CreateResponse<string>(message: "Insufficient Funds!", data: null, status: false);
                }

                //deducts the withdrawal amount from the available balance
                newBalance = currentBalance - details.Amount;

                //updates the balance in teh database
                var response = await _currencyRepository.DepositOrWithdraw(currencyId: details.CurrencyId, newBalance: newBalance);
                if (response > 0)
                {
                    transactionRecordObject.Amount = details.Amount;
                    transactionRecordObject.BeneficiaryAddress = details.BeneficiaryAddress;
                    transactionRecordObject.BeneficiaryName = details.BeneficiaryName;
                    transactionRecordObject.CurrencyShortCode = response1.Data.Code;
                    transactionRecordObject.FinancialInstitutionType = details.FinancialInstitutionType;
                    transactionRecordObject.InstitutionName = details.InstitutionName;
                    transactionRecordObject.Remark = "successful";
                    transactionRecordObject.TransactionType = "debit";
                    transactionRecordObject.WalletId = response1.Data.WalletId;
                    transactionRecordObject.WalletCurrencyId = response1.Data.Id;

                    await _transactionService.LogDebitTransactions(transactionRecordObject);

                    return ResponseHelper.CreateResponse<string>(message: "Transaction Successful", data: null, status: true);
                    
                }

                //logs failed transactions
                transactionRecordObject.Amount = details.Amount;
                transactionRecordObject.BeneficiaryAddress = details.BeneficiaryAddress;
                transactionRecordObject.BeneficiaryName = details.BeneficiaryName;
                transactionRecordObject.CurrencyShortCode = response1.Data.Code;
                transactionRecordObject.FinancialInstitutionType = details.FinancialInstitutionType;
                transactionRecordObject.InstitutionName = details.InstitutionName;
                transactionRecordObject.Remark = "failed";
                transactionRecordObject.TransactionType = "debit";
                transactionRecordObject.WalletId = response1.Data.WalletId;
                transactionRecordObject.WalletCurrencyId = response1.Data.Id;

                await _transactionService.LogDebitTransactions(transactionRecordObject);

                return ResponseHelper.CreateResponse<string>(message: "Something went wrong. Transaction Unsuccessful", data: null, status: false);
            }
            catch (Exception e)
            {
                return res = ResponseHelper.CreateResponse<string>(message: "Error", data: null, status: true,error:e);
            }

            


        }
    }
}
