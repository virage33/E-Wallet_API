
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
//using Ewallet.DataAccess.Implementations;
using Ewallet.DataAccess.EntityFramework.Implementations;
//using Ewallet.DataAccess.Interfaces;
using Ewallet.Core.Interfaces;
using Ewallet.Core.Implementations;
using Ewallet.Core.JWT.Implementations;
using Ewallet.Core.JWT.Interfaces;
using Ewallet.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Ewallet.Models;
using Ewallet.DataAccess.EntityFramework.Interfaces;

namespace EwalletApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //auth
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            //wallet
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IWalletServices, WalletService>();
            //Currency
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            //transactions
            services.AddScoped<ITransactionRepository, TransactionsRepository>();
            //currency converter
            services.AddScoped<ICurrencyConversionService, CurrencyConversionService>();
            //entity framework dbContext
            services.AddDbContextPool<EwalletContext>( options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<EwalletContext>();
            
            //seeder class
            services.AddTransient<Seeder>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();

            services.AddMvc().AddNewtonsoftJson();

            services.AddControllers();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option => {
                var param = new TokenValidationParameters();
                param.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTKey"]));
                param.ValidateIssuer = false;
                param.ValidateAudience = false;
                option.TokenValidationParameters = param;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EwalletApi", Version = "v1" });
                c.AddSecurityDefinition(name: "Bearer", new OpenApiSecurityScheme { 
                    Name="Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme="Bearer",
                    BearerFormat="JWT",
                    In = ParameterLocation.Header,
                    Description = "E-Wallet API"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seeder seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EwalletApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            seed.Seed().Wait();
        }
    }
}
