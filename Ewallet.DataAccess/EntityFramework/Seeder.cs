using Ewallet.Models;
using Ewallet.Models.AccountModels;
using EwalletApi.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.DataAccess.EntityFramework
{
    public class Seeder  
    {
        private readonly EwalletContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

      

        public Seeder(EwalletContext ctx, UserManager<AppUser> userManager, RoleManager<IdentityRole> role)
        {
           
            context = ctx;
            this.userManager = userManager;
            this.roleManager = role;
        }

        public async Task Seed()
        {
            try
            {

                context.Database.EnsureCreated();
                var roles = new string[] { "admin", "elite", "noob" };
                if (!roleManager.Roles.Any())
                {
                    foreach (var role in roles)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
                context.SaveChanges();


                //var currency = System.IO.File.ReadAllText(@$"{directoryInfo.FullName}CurrencySeedData.json");
                //var serializedCurrencyData = JsonConvert.DeserializeObject<List<Currency>>(currency);

                var currencyData = new List<Currency> { new Currency { Code = "NGN", Type = "naira" }, 
                    new Currency { Code = "USD", Type = "dollar" }, 
                    new Currency { Code = "GBP", Type = "pound" }, 
                    new Currency { Code = "EUR", Type = "euro" },
                    new Currency { Code = "CAD", Type = "cad dollar" },
                    new Currency { Code = "YEN", Type = "jp yen" },
                    new Currency { Code = "RND", Type = "rand" },
                };
                if (!context.Currency.Any())
                {
                   foreach(var cur in currencyData)
                    {
                        await context.Currency.AddAsync(cur);
                    }
                }

                context.SaveChanges();



                //user data
                var userData = new List<AppUser> {
                new AppUser{
                    
                    FirstName = "Suleiman",
                    LastName= "Sani",
                    Email= "suleiman.sani1@gmail.com",
                    password= "_ReptileMk4",
                    PhoneNumber= "+234 (895) 542-3137",
                    IsActive= true,
                    Wallet = null,
                    
                },
                 new AppUser{
                    Id= "31048d2a-0788-4503-8ab7-79281ec6765b",
                    FirstName= "TestUser1",
                    LastName= "TestLastName",
                    Email= "testemail@gmail.com",
                    password= "P@ssword123",
                    PhoneNumber= "+234 (895) 542-3137",
                    IsActive= true,
                    Wallet = new List<WalletModel>
                    {
                        new WalletModel
                        {
                            Id= "b9832663-20ce-46ff-97e9-ef4bdf91599c",
                            MainCurrency= "NGN",
                            WalletBalance = 1882.99M,
                            Transactions= new List<Transactions>{ },
                            UserId= "31048d2a-0788-4503-8ab7-79281ec6765b",
                            Currency= new List<WalletCurrency>{
                                new WalletCurrency{
                                    Id= "a552bc23-c946-44b3-ae76-d023d4de00cc",
                                    Currencybalance = 1129.81M,
                                    IsMain = true,
                                    WalletId= "b9832663-20ce-46ff-97e9-ef4bdf91599c",
                                    CurrencyId= 1
                                }   
                            }

                        }
                    }
                },

                };
              //  var data = System.IO.File.ReadAllText(@$"{directoryInfo.FullName}/SeedData.json");
                //var serializedData = JsonConvert.DeserializeObject<List<AppUser>>(data);


                if (!userManager.Users.Any())
                {
                    var role = "";
                    int counter = 0;
                    foreach (var user in userData)
                    {
                        user.UserName = user.Email;
                        role = counter < 2 ? roles[0] : roles[1];

                        var res = await userManager.CreateAsync(user,user.password);
                        if (res.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, role);
                        }
                        counter++;
                    }
                }
                context.SaveChanges();
            }
            catch (Exception )
            {

                throw;
            }
        }
    }
}
