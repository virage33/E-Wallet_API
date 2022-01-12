using Ewallet.Models;
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
        private readonly string path = @"C:\Users\hp\source\repos\EwalletApi\Ewallet.DataAccess\EntityFramework\";
        private readonly string fpath = @"C:\Users\hp\source\repos\EwalletApi\Ewallet.DataAccess\EntityFramework\SeedData.json";
        private readonly string cfpath = @"C:\Users\hp\source\repos\EwalletApi\Ewallet.DataAccess\EntityFramework\CurrencySeedData.json";
        private DirectoryInfo directoryInfo;
        private string dpath;
        private FileInfo fileInfo;
      

        public Seeder(EwalletContext ctx, UserManager<AppUser> userManager, RoleManager<IdentityRole> role)
        {
            dpath= Directory.GetCurrentDirectory();
            directoryInfo = new DirectoryInfo(path);
            fileInfo = new FileInfo(fpath);
            if (!directoryInfo.Exists){
                directoryInfo.Create();
            }
            if (!fileInfo.Exists)
            {
                fileInfo.Create();
            }
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


                var currency = System.IO.File.ReadAllText(@$"{directoryInfo.FullName}CurrencySeedData.json");
                var serializedCurrencyData = JsonConvert.DeserializeObject<List<Currency>>(currency);
                if (!context.Currency.Any())
                {
                   foreach(var cur in serializedCurrencyData)
                    {
                        await context.Currency.AddAsync(cur);
                    }
                }

                var data = System.IO.File.ReadAllText(@$"{directoryInfo.FullName}SeedData.json");
                var serializedData = JsonConvert.DeserializeObject<List<AppUser>>(data);
                context.SaveChanges();

                if (!userManager.Users.Any())
                {
                    var role = "";
                    int counter = 0;
                    foreach (var user in serializedData)
                    {
                        user.UserName = user.Email;
                        role = counter < 2 ? roles[0] : roles[1];

                        var res = await userManager.CreateAsync(user, "P@ssword123");
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
