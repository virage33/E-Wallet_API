using Ewallet.Models;
using Ewallet.Models.DTO;
using Ewallet.Models.DTO.WalletDTO;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface IWalletServices
    {
        Task<string> CreateWallet(string uid, string maincurrency);
        Task<string> DeleteWallet(string walletId);    
        Task<ResponseDTO<string>> AddCurrency(string walletId, string currencyCode, bool isMain = false);
        Task<bool> RemoveCurrency(string currencyId);
        Task<List<WalletDTO>> GetAllUserWallets(string uid);
        Task<WalletDTO> GetWallet(string walletId);
        Task<ResponseDTO<decimal>> WalletBalance(string walletId);

    }
}
