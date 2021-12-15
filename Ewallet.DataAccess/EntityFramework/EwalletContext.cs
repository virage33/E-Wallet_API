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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var configuration = new ConfigurationBuilder();
        //    optionsBuilder.UseSqlServer(connectionString: "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=EwalletDb;Integrated Security=True");
        //}

        public EwalletContext(DbContextOptions<EwalletContext> options)
            : base(options)
        {
        }


    }
}
