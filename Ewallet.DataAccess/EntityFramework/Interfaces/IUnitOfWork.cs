using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.EntityFramework.Interfaces
{
    public interface IUnitOfWork
    {
        IWalletRepository WalletRepository { get;  }
        ICurrencyRepository CurrencyRepository { get; }
        ITransactionRepository TransactionRepository { get;}
        Task<int> Save();
    }
}
