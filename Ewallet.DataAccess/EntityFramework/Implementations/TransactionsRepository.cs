
using Ewallet.DataAccess.EntityFramework.Interfaces;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Ewallet.Models.AccountModels;

namespace Ewallet.DataAccess.EntityFramework.Implementations
{
    public class TransactionsRepository : ITransactionRepository
    {
        private readonly EwalletContext context;

        public TransactionsRepository(EwalletContext context)
        {
            this.context = context;
        }
        //gets all credit transactions for admin only
        public async Task<List<Transactions>> GetAllCreditTransactions()
        {
            List<Transactions> result = new List<Transactions>();
            var response = from t in context.Transactions
                           join ct in context.CreditTransactions on t.TransactionsId equals ct.CreditTransactionsId
                           select new
                           {
                               t.TransactionsId,
                               t.WalletId,
                               t.WalletCurrencyId,
                               t.Date,
                               t.CurrencyShortCode,
                               t.Amount,
                               t.TransactionType,
                               t.Remark,
                               ct.DestinationCurrencyAddress
                           };

            foreach (var item in response)
            {
                result.Add(
                    new Transactions
                    {
                        Amount = item.Amount,
                        Date = item.Date,
                        CurrencyShortCode = item.CurrencyShortCode,
                        WalletId = item.WalletId,
                        WalletCurrencyId= item.WalletCurrencyId,
                        TransactionsId = item.TransactionsId,
                        TransactionType = item.TransactionType,
                        Remark = item.Remark,
                        CreditTransactions = new CreditTransactions
                        {
                            DestinationCurrencyAddress = item.DestinationCurrencyAddress,
                            CreditTransactionsId = item.TransactionsId,
                            DestinationWalletAddress = item.WalletId,
                        }
                    }
                    );
            }
            return result;
        }

        //gets all credit transactions for a particular wallet currency
        public async Task<List<Transactions>> GetAllCurrencyCreditTransactions(string currencyId)
        {
            List<Transactions> result = new List<Transactions>();
            var response = from t in context.Transactions
                           join ct in context.CreditTransactions on t.TransactionsId equals ct.CreditTransactionsId
                           where ct.DestinationCurrencyAddress == currencyId
                           select new
                           {
                               t.TransactionsId,
                               t.WalletId,
                               t.WalletCurrencyId,
                               t.Date,
                               t.CurrencyShortCode,
                               t.Amount,
                               t.TransactionType,
                               t.Remark,
                               ct.DestinationCurrencyAddress
                           };
            foreach(var item in response)
            {
                result.Add(
                    new Transactions
                    {
                        Amount = item.Amount,
                        Date = item.Date,
                        CurrencyShortCode = item.CurrencyShortCode,
                        WalletId = item.WalletId,
                        WalletCurrencyId= item.WalletCurrencyId,
                        TransactionsId = item.TransactionsId,
                        TransactionType=item.TransactionType,
                        Remark = item.Remark,
                        CreditTransactions = new CreditTransactions
                        {
                            DestinationCurrencyAddress = item.DestinationCurrencyAddress,
                            CreditTransactionsId = item.TransactionsId,
                            DestinationWalletAddress = item.WalletId,
                        }
                    }
                    );
            }

            return result;
        }

        //gets all debit transactions for a particular wallet currency
        public async Task<List<Transactions>> GetAllCurrencyDebitTransactions(string currencyId)
        {
            List<Transactions> result = new List<Transactions>();
            var response = from t in context.Transactions
                           join dt in context.DebitTransactions on t.TransactionsId equals dt.DebitTransactionsId
                           where t.WalletCurrencyId == currencyId
                           select new
                           {
                               t.TransactionsId,
                               t.WalletId,
                               t.WalletCurrencyId,
                               t.Date,
                               t.CurrencyShortCode,
                               t.Amount,
                               t.TransactionType,
                               t.Remark,
                               dt.BeneficiaryAddress,
                               dt.BeneficiaryName,
                               dt.FinancialInstitutionType,
                               dt.InstitutionName
                           };
            foreach (var item in response)
            {
                result.Add(
                    new Transactions
                    {
                        Amount = item.Amount,
                        Date = item.Date,
                        CurrencyShortCode = item.CurrencyShortCode,
                        WalletId = item.WalletId,
                        WalletCurrencyId = item.WalletCurrencyId,
                        TransactionsId = item.TransactionsId,
                        TransactionType = item.TransactionType,
                        Remark = item.Remark,
                        DebitTransactions = new DebitTransactions
                        {
                            BeneficiaryAddress = item.BeneficiaryAddress,
                            BeneficiaryName= item.BeneficiaryName,
                            FinancialInstitutionType = item.FinancialInstitutionType,
                            InstitutionName = item.InstitutionName,
                        }
                    }
                    );
            }
            return result;
        }

        //gets all transactions for a particular user wallet currency
        public async Task<List<Transactions>> GetAllCurrencyTransactions(string currencyId)
        {
            List<Transactions> result = new List<Transactions>();
            var response = from t in context.Transactions
                           where t.WalletCurrencyId == currencyId
                           select new
                           {
                               t.TransactionsId,
                               t.TransactionType,
                               t.WalletCurrencyId,
                               t.WalletId,
                               t.Remark,
                               t.Amount,
                               t.Date,
                               t.CurrencyShortCode,
                           };
            foreach(var item in response)
            {
                result.Add(new Transactions { 
                    TransactionsId=item.TransactionsId,
                    WalletId = item.WalletId,
                    WalletCurrencyId=item.WalletCurrencyId,
                    TransactionType = item.TransactionType,
                    Amount = item.Amount,
                    Date = item.Date,
                    Remark = item.Remark,
                    CurrencyShortCode = item.CurrencyShortCode,
                    
                });
            }
            return result;
        }

        //gets all transfer transactions for a particular currency
        public async Task<List<Transactions>> GetAllCurrencyTransferTransactions(string currencyId)
        {
            List<Transactions> result = new List<Transactions>();
            var response = context.Transactions.Where(x => x.WalletCurrencyId == currencyId && context.TransferTransactions.Any(y => y.TransferTransactionsId == x.TransactionsId));
            var transf = context.TransferTransactions.Where(x => response.Any(y => y.TransactionsId == x.TransferTransactionsId));

            foreach (var item in response)
            {
                result.Add(
                    new Transactions
                    {
                        TransactionsId = item.TransactionsId,
                        Amount = item.Amount,
                        WalletId = item.WalletId,
                        WalletCurrencyId = item.WalletCurrencyId,
                        Date = item.Date,
                        Remark = item.Remark,
                        CurrencyShortCode = item.CurrencyShortCode,
                        TransferTransactions = transf.FirstOrDefault(x => x.TransferTransactionsId == item.TransactionsId)
                    });
            }
            return result;
        }

        //Gets all debit transactions for admin only
        public async Task<List<Transactions>> GetAllDebitTransactions()
        {
            List<Transactions> result = new List<Transactions>();
            var response = from t in context.Transactions
                           join dt in context.DebitTransactions on t.TransactionsId equals dt.DebitTransactionsId
                           select new
                           {
                               t.TransactionsId,
                               t.WalletId,
                               t.WalletCurrencyId,
                               t.Date,
                               t.CurrencyShortCode,
                               t.Amount,
                               t.TransactionType,
                               t.Remark,
                               dt.BeneficiaryAddress,
                               dt.BeneficiaryName,
                               dt.FinancialInstitutionType,
                               dt.InstitutionName
                           };
            foreach (var item in response)
            {
                result.Add(
                    new Transactions
                    {
                        Amount = item.Amount,
                        Date = item.Date,
                        CurrencyShortCode = item.CurrencyShortCode,
                        WalletId = item.WalletId,
                        WalletCurrencyId = item.WalletCurrencyId,
                        TransactionsId = item.TransactionsId,
                        TransactionType = item.TransactionType,
                        Remark = item.Remark,
                        DebitTransactions = new DebitTransactions
                        {
                            BeneficiaryAddress = item.BeneficiaryAddress,
                            BeneficiaryName = item.BeneficiaryName,
                            FinancialInstitutionType = item.FinancialInstitutionType,
                            InstitutionName = item.InstitutionName,
                        }
                    }
                    );
            }
            return result;
        }

        //gets all transactions for all users ... admin only
        public async Task<List<Transactions>> GetAllTransactions()
        {
            List<Transactions> result = new List<Transactions>();
            var response = context.Transactions;
            await foreach (var item in response)
            {
                //result.Add(new Transactions
                //{
                //    WalletId = item.WalletId,
                //    WalletCurrencyId = item.WalletCurrencyId,
                //    TransactionType = item.TransactionType,
                //    Remark = item.Remark,
                //    Date = item.Date,
                //    Amount = item.Amount,
                //    CurrencyShortCode = item.CurrencyShortCode,
                //    TransactionsId = item.TransactionsId,
                //    CreditTransactions = item.CreditTransactions,
                //    DebitTransactions = item.DebitTransactions,
                //    TransferTransactions = item.TransferTransactions
                //});
                result.Add(item);
            }
            return result;
        }

        //gets all transfer transactionsfor all users...admin only
        public async Task<List<Transactions>> GetAllTransferTransactions()
        {
            List<Transactions> result = new List<Transactions>();
            var response = context.Transactions.Where(x => context.TransferTransactions.Any(y => y.TransferTransactionsId == x.TransactionsId));
            var transf = context.TransferTransactions.Where(x => response.Any(y => y.TransactionsId == x.TransferTransactionsId));

            foreach (var item in response)
            {
                result.Add(
                    new Transactions
                    {
                        TransactionsId = item.TransactionsId,
                        Amount = item.Amount,
                        WalletId = item.WalletId,
                        WalletCurrencyId = item.WalletCurrencyId,
                        Date = item.Date,
                        Remark = item.Remark,
                        CurrencyShortCode = item.CurrencyShortCode,
                        TransferTransactions = transf.FirstOrDefault(x => x.TransferTransactionsId == item.TransactionsId)
                    });
            }
            return result;
        }



        //gets all credit transactions in a user wallet
        public async Task<List<Transactions>> GetAllWalletCreditTransactions(string walletId)
        {
            List<Transactions> result = new List<Transactions>();
            var response = from t in context.Transactions
                           join ct in context.CreditTransactions on t.TransactionsId equals ct.CreditTransactionsId
                           where t.WalletId == walletId
                           select new
                           {
                               t.TransactionsId,
                               t.WalletId,
                               t.WalletCurrencyId,
                               t.Date,
                               t.CurrencyShortCode,
                               t.Amount,
                               t.TransactionType,
                               t.Remark,
                               ct.DestinationCurrencyAddress
                           };
            foreach (var item in response)
            {
                result.Add(
                    new Transactions
                    {
                        Amount = item.Amount,
                        Date = item.Date,
                        CurrencyShortCode = item.CurrencyShortCode,
                        WalletId = item.WalletId,
                        WalletCurrencyId = item.WalletCurrencyId,
                        TransactionsId = item.TransactionsId,
                        TransactionType = item.TransactionType,
                        Remark = item.Remark,
                        CreditTransactions = new CreditTransactions
                        {
                            DestinationCurrencyAddress = item.DestinationCurrencyAddress,
                            CreditTransactionsId = item.TransactionsId,
                            DestinationWalletAddress = item.WalletId,
                        }
                    }
                    );
            }

            return result;
        }

        //gets all debit transactions on a wallet
        public async Task<List<Transactions>> GetAllWalletDebitTransactions(string walletId)
        {
            List<Transactions> result = new List<Transactions>();
            var response = from t in context.Transactions
                           join dt in context.DebitTransactions on t.TransactionsId equals dt.DebitTransactionsId
                           where t.WalletId == walletId
                           select new
                           {
                               t.TransactionsId,
                               t.WalletId,
                               t.WalletCurrencyId,
                               t.Date,
                               t.CurrencyShortCode,
                               t.Amount,
                               t.TransactionType,
                               t.Remark,
                               dt.BeneficiaryAddress,
                               dt.BeneficiaryName,
                               dt.FinancialInstitutionType,
                               dt.InstitutionName
                           };
            foreach (var item in response)
            {
                result.Add(
                    new Transactions
                    {
                        Amount = item.Amount,
                        Date = item.Date,
                        CurrencyShortCode = item.CurrencyShortCode,
                        WalletId = item.WalletId,
                        WalletCurrencyId = item.WalletCurrencyId,
                        TransactionsId = item.TransactionsId,
                        TransactionType = item.TransactionType,
                        Remark = item.Remark,
                        DebitTransactions = new DebitTransactions
                        {
                            BeneficiaryAddress = item.BeneficiaryAddress,
                            BeneficiaryName = item.BeneficiaryName,
                            FinancialInstitutionType = item.FinancialInstitutionType,
                            InstitutionName = item.InstitutionName,
                        }
                    }
                    );
            }
            return result;
        }

        //gets all transactions on a particular wallet
        public async Task<List<Transactions>> GetAllWalletTransactions(string walletid)
        {
            var response = context.Transactions.Where(x => x.WalletId == walletid).ToList<Transactions>();
            return response;
        }

        //gets all transfer transactions in a wallet
        public async Task<List<Transactions>> GetAllWalletTransferTransactions(string walletId)
        {
            List<Transactions> result = new List<Transactions>();
            var response = context.Transactions.Where(x => x.WalletId ==walletId && context.TransferTransactions.Any(y=>y.TransferTransactionsId==x.TransactionsId));
            var transf = context.TransferTransactions.Where(x => response.Any(y => y.TransactionsId == x.TransferTransactionsId));

            foreach (var item in response)
            {          
                result.Add(
                    new Transactions
                    {
                        TransactionsId=item.TransactionsId,
                        Amount= item.Amount,
                        WalletId = item.WalletId,
                        WalletCurrencyId= item.WalletCurrencyId,
                        Date=item.Date,
                        Remark = item.Remark,
                        CurrencyShortCode = item.CurrencyShortCode,                      
                        TransferTransactions = transf.FirstOrDefault(x=>x.TransferTransactionsId==item.TransactionsId)
                    });
            }
            return result;
        }

        //gets a particular transaction by the transaction id
        public async Task<Transactions> GetTransaction(string transactonId)
        {
            var response = await context.Transactions.FindAsync(transactonId);
            if (response.TransactionType == "credit")
            {
                response.CreditTransactions = new CreditTransactions();
                var val = await context.CreditTransactions.FindAsync(response.TransactionsId);
                response.CreditTransactions.CreditTransactionsId = val.CreditTransactionsId;
                response.CreditTransactions.DestinationCurrencyAddress = val.DestinationCurrencyAddress;
                response.CreditTransactions.DestinationWalletAddress = val.DestinationWalletAddress;
                
            }
            else if (response.TransactionType == "debit")
            {
                response.DebitTransactions = await context.DebitTransactions.FindAsync(response.TransactionsId);
            }
            else if (response.TransactionType == "transfer")
            {
                response.TransferTransactions = await context.TransferTransactions.FindAsync(response.TransactionsId);
            }
        
            return response;
        }

        //registers a transaction
        public async Task<Transactions> LogTransactions(Transactions Deposit)
        {        
                var res = await context.AddAsync<Transactions>(Deposit);
                await context.SaveChangesAsync();
                return res.Entity;
        }

       
    }
}
