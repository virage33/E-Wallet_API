
using Ewallet.DataAccess.EntityFramework.Interfaces;
using Ewallet.Models.AccountModels;
using Ewallet.Models.DTO;
using EwalletApi.Models.AccountModels;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.EntityFramework.Implementations
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly EwalletContext context;

        public CurrencyRepository(EwalletContext context)
        {
            this.context = context;
        }
        public async Task<int> CreateCurrency(WalletCurrency data, string code)
        {
            
                var currencyShortCode = context.Currency.Where(x => x.Code == code.ToUpper()).FirstOrDefault();
                if (currencyShortCode is Currency)
                {
                    data.CurrencyId = currencyShortCode.Id;
                    var response = await context.WalletCurrency.AddAsync(data);
                    context.SaveChanges();
                    return 1;
                }
                return 0;       
        }

        public Task<int> DeleteCurrency(string Id)
        {
            var response = context.WalletCurrency.Where(x => x.Id == Id).FirstOrDefault();
            if (response is WalletCurrency)
            {
                context.Remove(response);
            }
            context.SaveChanges();

            return Task.FromResult(1);
        }

        public async Task<int> DepositOrWithdraw(string currencyId, decimal newBalance)
        {
            var res = await context.WalletCurrency.FindAsync(currencyId);
            res.Currencybalance = newBalance;
            var response = context.WalletCurrency.Update(res);
            context.SaveChanges();
            return 1;
        }

        public Task<List<CurrencyDTO>> GetAllCurrencies(string walletId)
        {
            var result = new List<CurrencyDTO>();

            var currencies = from wc in context.WalletCurrency
                             join c in context.Currency on wc.CurrencyId equals c.Id
                             where wc.WalletId == walletId
                             select new
                             {
                                 wc.Id,
                                 wc.IsMain,
                                 wc.Currencybalance,
                                 wc.WalletId,
                                 c.Type,
                                 c.Code,
                             };

            foreach (var item in currencies)
            {
                var cur = new CurrencyDTO();
                cur.WalletId = item.WalletId;
                cur.IsMain = item.IsMain;
                cur.Type = item.Type;
                cur.Code = item.Code;
                cur.Balance = item.Currencybalance;
                cur.Id = item.Id;
                result.Add(cur);
            }
            
            return Task.FromResult(result);
        }

        public Task<CurrencyDTO> GetCurrency(string currencyId)
        {
            var cur = new CurrencyDTO();
            var items = from wc in context.WalletCurrency
            join c in context.Currency on wc.CurrencyId equals c.Id where wc.Id == currencyId
            select new
            {
                wc.Id,
                wc.IsMain,
                wc.Currencybalance,
                wc.WalletId,
                c.Type,
                c.Code,
            };
             foreach(var item in items)
            {
                cur.WalletId = item.WalletId;
                cur.IsMain = item.IsMain;
                cur.Type = item.Type;
                cur.Code = item.Code;
                cur.Balance = item.Currencybalance;
                cur.Id = item.Id;
            }

            return Task.FromResult(cur);
        }
    }
}
