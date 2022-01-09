using Ewallet.Models;
using Ewallet.Models.AccountModels;
using EwalletApi.Models;
using EwalletApi.Models.AccountModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.DataAccess.EntityFramework
{
    public class EwalletContext:IdentityDbContext<AppUser>
    {
          
        public DbSet<Currency> Currency { get; set; }
        public DbSet<WalletModel> Wallet { get; set; }
        public DbSet<WalletCurrency> WalletCurrency { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<CreditTransactions> CreditTransactions { get; set; }
        public DbSet<DebitTransactions> DebitTransactions { get; set; }
        public DbSet<TransferTransactions> TransferTransactions { get; set; }
        public DbSet<BlacklistedTokens> BlacklistedTokens { get; set; } 

        public EwalletContext(DbContextOptions<EwalletContext> options)
            : base(options)
        {
        }


    }
}
