using Ewallet.DataAccess.EntityFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.EntityFramework.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly EwalletContext _ewalletContext;
        private IWalletRepository _WalletRepository;
        public ICurrencyRepository _CurrencyRepository;
        public ITransactionRepository _TransactionRepository;
        public UnitOfWork(EwalletContext ewalletContext)
        {
            _ewalletContext = ewalletContext;
        }

        public IWalletRepository WalletRepository { 
            get {
                return _WalletRepository = _WalletRepository ?? new WalletRepository(_ewalletContext);
            }  }

        public ICurrencyRepository CurrencyRepository { 
            get {
                return _CurrencyRepository = _CurrencyRepository ?? new CurrencyRepository(_ewalletContext);
            } }
        public ITransactionRepository TransactionRepository { get => _TransactionRepository = _TransactionRepository ?? new TransactionsRepository(_ewalletContext); }

        public async Task<int> Save()
        {  
             return await _ewalletContext.SaveChangesAsync();    
        }
     
    }
}
