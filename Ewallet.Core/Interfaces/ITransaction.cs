using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface ITransaction
    {
        Task GetAllTransactions();
        Task RegisterTransaction();
        Task CreditTransactions();
        Task DebitTransactions();

    }
}
