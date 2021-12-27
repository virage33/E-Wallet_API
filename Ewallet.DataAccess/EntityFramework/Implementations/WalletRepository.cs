
using Ewallet.DataAccess.EntityFramework.Interfaces;
using EwalletApi.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.EntityFramework.Implementations
{
    public class WalletRepository : IWalletRepository
    {
        private readonly EwalletContext context;

        public WalletRepository(EwalletContext context)
        {
            this.context = context;
        }
        public async Task<int> CreateWallet(WalletModel data)
        {

                 await context.Wallet.AddAsync(data);
                 await context.SaveChangesAsync();
           
            return 1;
            
        }

        public async Task<int> DeleteWallet(string walletId)
        {
            
                var response = context.Wallet.Where(x => x.Id == walletId).FirstOrDefault();
                if (response is WalletModel)
                {
                    context.Remove(response);
                }
                await context.SaveChangesAsync();
             
            return 1;
            
        }

            public async Task<List<WalletModel>> GetAllUserWallets(string uid)
        {
                var result = new List<WalletModel>();


            var wallets = context.Wallet.Where(x=>x.UserId == uid);

            foreach (var item in wallets)
                    {
                        result.Add(item);
                    }
                
                return result;
                
        }

        public async Task<WalletModel> GetIndividualUserWallet(string walletId)
        {
                        
            var response = context.Wallet.Where(x => x.Id == walletId);
            
            return response.FirstOrDefault();
           
        }

        public async Task<int> UpdateWalletBalance(string walletId, decimal balance)
        {
            var wallet = await context.Wallet.FindAsync(walletId);
            wallet.WalletBalance = balance;
            var res = context.Wallet.Update(wallet);
            return await context.SaveChangesAsync();
        }

    }
}
