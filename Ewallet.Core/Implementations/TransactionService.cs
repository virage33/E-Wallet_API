using AutoMapper;
using Ewallet.Commons;
using Ewallet.Core.Interfaces;
using Ewallet.DataAccess.EntityFramework.Interfaces;
using Ewallet.Models.AccountModels;
using Ewallet.Models.DTO;
using Ewallet.Models.DTO.ReturnDTO;
using Ewallet.Models.DTO.WalletDTO;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ewallet.Core.Implementations
{
    public class TransactionService : ITransaction
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            this._transactionRepository = transactionRepository;
            this._mapper = mapper;
        }

        //logs credit transactions
        public async Task<ResponseDTO<Transactions>> LogCreditTransactions(CreditTransactionDTO data)
        {
            var transaction = new Transactions();
            transaction.Amount = data.Amount;
            transaction.Remark = data.Remark;
            transaction.WalletCurrencyId = data.WalletCurrencyId;
            transaction.WalletId = data.DestinationWalletAddress;
            transaction.TransactionType = data.TransactionType;
            transaction.CurrencyShortCode = data.CurrencyShortCode;
            transaction.CreditTransactions = new CreditTransactions {
                CreditTransactionsId = data.TransactionsId,
                DestinationWalletAddress = data.DestinationWalletAddress,
                DestinationCurrencyAddress = data.DestinationCurrencyAddress
            };

            var res = await _transactionRepository.LogTransactions(transaction);
            if (res != null)
                return ResponseHelper.CreateResponse<Transactions>("successful", data: res, status: true);
            
            return ResponseHelper.CreateResponse<Transactions>("failed", data: res, status: false);

        }

        //gets all credit transactions on a particullar wallet currency 
        public async Task<ResponseDTO<List<CreditTransactionReturnDTO>>> GetAllCurrencyCreditTransactions(string currencyId)
        {
            try
            {
                List<CreditTransactionReturnDTO> result = new List<CreditTransactionReturnDTO>();
                var response = await _transactionRepository.GetAllCurrencyCreditTransactions(currencyId);
                if (response.Count >0)
                {
                    foreach (var item in response)
                    {
                        //maps the repository response to the return dto
                        result.Add(_mapper.Map<CreditTransactionReturnDTO>(item));
                    }
                    return ResponseHelper.CreateResponse<List<CreditTransactionReturnDTO>>(message: "successful", data: result, status: true);
                }
                return ResponseHelper.CreateResponse<List<CreditTransactionReturnDTO>>(message: "no records exist", data: result, status: false);
            }
            catch (Exception e)
            {

                return ResponseHelper.CreateResponse<List<CreditTransactionReturnDTO>>(message: "error", data: null, status: false,e);
            }
        }

        
        //get all transactions on the system ...for admin only
        public async Task<ResponseDTO<List<TransactionReturnDTO>>> GetAllTransactions()
        {
            try
            {
                List<TransactionReturnDTO> result = new List<TransactionReturnDTO>();
                var response = await _transactionRepository.GetAllTransactions();
                if(response!= null)
                {
                    foreach (Transactions item in response)
                    {
                        //maps the response to the return DTO 
                        var a = _mapper.Map<TransactionReturnDTO>(item);
                        result.Add(a);

                    }
                    return ResponseHelper.CreateResponse<List<TransactionReturnDTO>>(message: "successful", data: result, status: true);
                }
                return ResponseHelper.CreateResponse<List<TransactionReturnDTO>>(message: "no records exist!", data: result, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<List<TransactionReturnDTO>>(message: "error", data: null, status: false, e);
            } 
            
        }

        //records debit transactions
        public async Task<ResponseDTO<Transactions>> LogDebitTransactions(DebitTransactionDTO data)
        {
            try
            {
                var transaction = new Transactions();
                transaction.Amount = data.Amount;
                transaction.Remark = data.Remark;
                transaction.WalletCurrencyId = data.WalletCurrencyId;
                transaction.WalletId = data.WalletId;
                transaction.TransactionType = data.TransactionType;
                transaction.CurrencyShortCode = data.CurrencyShortCode;
                transaction.DebitTransactions = new DebitTransactions
                {
                    BeneficiaryAddress = data.BeneficiaryAddress,
                    BeneficiaryName = data.BeneficiaryName,
                    DebitTransactionsId = data.TransactionsId,
                    FinancialInstitutionType = data.FinancialInstitutionType,
                    InstitutionName = data.InstitutionName
                };

                var res = await _transactionRepository.LogTransactions(transaction);
                if (res != null)
                    return ResponseHelper.CreateResponse<Transactions>(message: "success", status: true, data: res);
                return ResponseHelper.CreateResponse<Transactions>(message: "something went wrong!", status: false, data: res);
            }
            catch(Exception e)
            {
                return ResponseHelper.CreateResponse<Transactions>(message: "error", status: false, data: null, error: e);
            }
        }

        //records transfer transactions
        public async Task<ResponseDTO<Transactions>> LogTransferTransactions(TransferTransactionDTO data)
        {
            var transaction = new Transactions();
            transaction.Remark = data.Remark;
            transaction.TransactionType = data.TransactionType;
            transaction.Amount = data.Amount;
            transaction.Date = data.Date;
            transaction.CurrencyShortCode = data.CurrencyShortCode;
            transaction.WalletCurrencyId = data.WalletCurrencyId;
            transaction.WalletId = data.WalletId;
            transaction.TransferTransactions = new TransferTransactions
            {
                BeneficiaryId = data.BeneficiaryId,
                BeneficiaryName = data.BeneficiaryName,
                TransferTransactionsId = transaction.TransactionsId,
                BeneficiaryWalletAddress = data.BeneficiaryWalletAddress,
                SenderWalletAddress = data.SenderWalletAddress
            };

            var res = await _transactionRepository.LogTransactions(transaction);
            if(res!=null)
                return ResponseHelper.CreateResponse<Transactions>("successful", data: res, status: true);

            return ResponseHelper.CreateResponse<Transactions>("failed", data: res, status: false);
        }

        //deletes transactions
        public Task DeleteTransaction()
        {
            throw new NotImplementedException();
        }

        //gets all transactions in a particular wallet
        public async Task<ResponseDTO<List<TransactionReturnDTO>>> GetAllWalletTransactions(string walletid)
        {
            try
            {
                List<TransactionReturnDTO> result = new List<TransactionReturnDTO>();
                var res = await _transactionRepository.GetAllWalletTransactions(walletid);
                if (res.Count > 0)
                {
                    foreach(var item in res)
                    {
                        //maps reepository response to return dto
                        result.Add(_mapper.Map<TransactionReturnDTO>(item));
                    }
                    return ResponseHelper.CreateResponse<List<TransactionReturnDTO>>(message: "successful", status: true, data: result);
                }
                return ResponseHelper.CreateResponse<List<TransactionReturnDTO>>(message: "no records found", status: false, data:result);
                    
            }
            catch (Exception e)
            {

                return ResponseHelper.CreateResponse<List<TransactionReturnDTO>>(message: "error", status: false,data:null, error: e);
            }
        }

        //get all transactions on a particular wallet currency
        public async Task<ResponseDTO<List<TransactionReturnDTO>>> GetAllCurrencyTransactions(string currencyId)
        {
            try
            {
                List<TransactionReturnDTO> result = new List<TransactionReturnDTO>();
                var res = await _transactionRepository.GetAllCurrencyTransactions(currencyId);
                if (res.Count > 0)
                {
                    foreach (var item in res)
                    {
                        //maps repository response to return dto
                        result.Add(_mapper.Map<TransactionReturnDTO>(item));
                    }
                    return ResponseHelper.CreateResponse<List<TransactionReturnDTO>>(message: "successful", status: true, data: result);
                }
                return ResponseHelper.CreateResponse<List<TransactionReturnDTO>>(message: "no records exist", status: false, data: result);
            }
            catch (Exception e)
            {

                return ResponseHelper.CreateResponse<List<TransactionReturnDTO>>(message: "error", status: false, data: null, error:e);
            }
        }

        //gets full details on an individual transaction
        public async Task<ResponseDTO<object>> GetTransaction(string transactonId)
        {
            try
            {
                
                var res = await _transactionRepository.GetTransaction(transactonId);
                if (res!= null)
                {
                    return ResponseHelper.CreateResponse<object>(message: "successful", status: true, data: res) ;
                }
                return ResponseHelper.CreateResponse<object>(message: "no records exist", status: false, data: res);
            }
            catch (Exception e)
            {

                return ResponseHelper.CreateResponse<object>(message: "error", status: false, data: null,error:e);
            }
        }

        //gets all credit transactions on the system....admin only
        public async Task<ResponseDTO<List<CreditTransactionReturnDTO>>> GetAllCreditTransactions()
        {
            try
            {
                List<CreditTransactionReturnDTO> result = new List<CreditTransactionReturnDTO>();
                var res = await _transactionRepository.GetAllCreditTransactions();
                if (res.Count > 0)
                {
                    foreach(var item in res)
                    {
                        //maps repository response to return dto
                        result.Add(_mapper.Map<CreditTransactionReturnDTO>(item));
                    }
                    return ResponseHelper.CreateResponse<List<CreditTransactionReturnDTO>>(message: "successful", data: result, status: true);
                }
                return ResponseHelper.CreateResponse<List<CreditTransactionReturnDTO>>(message: "unsuccessful", data: result, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<List<CreditTransactionReturnDTO>>(message: "error", data: null, status: false, error:e);
            }
        }

        //gets all credit transactions in a particular wallet
        public async Task<ResponseDTO<List<CreditTransactionReturnDTO>>> GetAllWalletCreditTransactions(string walletid)
        {
            try
            {
                List<CreditTransactionReturnDTO> result = new List<CreditTransactionReturnDTO>();
                var res = await _transactionRepository.GetAllWalletCreditTransactions(walletid);
                if (res.Count > 0)
                {
                    foreach (var item in res)
                    {
                        //maps repository response to credit transaction return dto
                        result.Add(_mapper.Map<CreditTransactionReturnDTO>(item));
                    }
                    return ResponseHelper.CreateResponse<List<CreditTransactionReturnDTO>>(message: "successful", data: result, status: true);
                }
                return ResponseHelper.CreateResponse<List<CreditTransactionReturnDTO>>(message: "unsuccessful", data: result, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<List<CreditTransactionReturnDTO>>(message: "error", data: null, status: false, error: e);
            }
        }

       

        public async Task<ResponseDTO<List<DebitTransactionReturnDTO>>> GetAllDebitTransactions()
        {
            try
            {
                List<DebitTransactionReturnDTO> result = new List<DebitTransactionReturnDTO>();
                var res = await _transactionRepository.GetAllDebitTransactions();
                if (res.Count > 0)
                {
                    foreach (var item in res)
                    {
                        //maps repository response to debit transaction return dto
                        result.Add(_mapper.Map<DebitTransactionReturnDTO>(item));
                    }
                    return ResponseHelper.CreateResponse<List<DebitTransactionReturnDTO>>(message: "successful", data: result, status: true);
                }
                return ResponseHelper.CreateResponse<List<DebitTransactionReturnDTO>>(message: "unsuccessful", data: result, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<List<DebitTransactionReturnDTO>>(message: "error", data: null, status: false, error: e);
            }
        }

        public async Task<ResponseDTO<List<DebitTransactionReturnDTO>>> GetAllWalletDebitTransactions(string walletid)
        {
            try
            {
                List<DebitTransactionReturnDTO> result = new List<DebitTransactionReturnDTO>();
                var res = await _transactionRepository.GetAllWalletDebitTransactions(walletid);
                if (res.Count > 0)
                {
                    foreach (var item in res)
                    {
                        //maps repository response to debit transaction return dto
                        result.Add(_mapper.Map<DebitTransactionReturnDTO>(item));
                    }
                    return ResponseHelper.CreateResponse<List<DebitTransactionReturnDTO>>(message: "successful", data: result, status: true);
                }
                return ResponseHelper.CreateResponse<List<DebitTransactionReturnDTO>>(message: "unsuccessful", data: result, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<List<DebitTransactionReturnDTO>>(message: "error", data: null, status: false, error: e);
            }
        }

        public async Task<ResponseDTO<List<DebitTransactionReturnDTO>>> GetAllCurrencyDebitTransactions(string currencyId)
        {
            try
            {
                List<DebitTransactionReturnDTO> result = new List<DebitTransactionReturnDTO>();
                var res = await _transactionRepository.GetAllCurrencyDebitTransactions(currencyId);
                if (res.Count > 0)
                {
                    foreach (var item in res)
                    {
                        //maps repository response to debit transaction return dto
                        result.Add(_mapper.Map<DebitTransactionReturnDTO>(item));
                    }
                    return ResponseHelper.CreateResponse<List<DebitTransactionReturnDTO>>(message: "successful", data: result, status: true);
                }
                return ResponseHelper.CreateResponse<List<DebitTransactionReturnDTO>>(message: "unsuccessful", data: result, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<List<DebitTransactionReturnDTO>>(message: "error", data: null, status: false, error: e);
            }
        }


        public async Task<ResponseDTO<List<TransferTransactionReturnDTO>>> GetAllTransferTransactions()
        {
            try
            {
                List<TransferTransactionReturnDTO> result = new List<TransferTransactionReturnDTO>();
                var res = await _transactionRepository.GetAllDebitTransactions();
                if (res.Count > 0)
                {
                    foreach (var item in res)
                    {
                        //maps repository response to transfer return dto
                        result.Add(_mapper.Map<TransferTransactionReturnDTO>(item));
                    }
                    return ResponseHelper.CreateResponse<List<TransferTransactionReturnDTO>>(message: "successful", data: result, status: true);
                }
                return ResponseHelper.CreateResponse<List<TransferTransactionReturnDTO>>(message: "unsuccessful", data: result, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<List<TransferTransactionReturnDTO>>(message: "error", data: null, status: false, error: e);
            }
        }

        public async Task<ResponseDTO<List<TransferTransactionReturnDTO>>> GetAllWalletTransferTransactions(string walletid)
        {
            try
            {
                List<TransferTransactionReturnDTO> result = new List<TransferTransactionReturnDTO>();
                var res = await _transactionRepository.GetAllWalletTransferTransactions(walletid);
                if (res.Count > 0)
                {
                    foreach (var item in res)
                    {
                        //maps repository response to 
                        result.Add(_mapper.Map<TransferTransactionReturnDTO>(item));
                    }
                    return ResponseHelper.CreateResponse<List<TransferTransactionReturnDTO>>(message: "successful", data: result, status: true);
                }
                return ResponseHelper.CreateResponse<List<TransferTransactionReturnDTO>>(message: "unsuccessful", data: result, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<List<TransferTransactionReturnDTO>>(message: "error", data: null, status: false, error: e);
            }
        }

        public async Task<ResponseDTO<List<TransferTransactionReturnDTO>>> GetAllCurrencyTransferTransactions(string currencyId)
        {
            try
            {
                List<TransferTransactionReturnDTO> result = new List<TransferTransactionReturnDTO>();
                var res = await _transactionRepository.GetAllCurrencyTransferTransactions(currencyId);
                if (res.Count > 0)
                {
                    foreach (var item in res)
                    {
                        //maps repository response to transfer transaction return dto
                        result.Add(_mapper.Map<TransferTransactionReturnDTO>(item));
                    }
                    return ResponseHelper.CreateResponse<List<TransferTransactionReturnDTO>>(message: "successful", data: result, status: true);
                }
                return ResponseHelper.CreateResponse<List<TransferTransactionReturnDTO>>(message: "unsuccessful", data: result, status: false);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse<List<TransferTransactionReturnDTO>>(message: "error", data: null, status: false, error: e);
            }
        }
    }
}
