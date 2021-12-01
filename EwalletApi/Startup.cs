using EwalletApi.Services.AuthService.Interfaces;
using EwalletApi.UI.Services;
using EwalletApi.UI.Services.AuthService.Implementations;
using EwalletApi.UI.Services.AuthService.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ewallet.DataAccess.Implementations;
using Ewallet.DataAccess.Interfaces;
using Ewallet.Core.Interfaces;
using Ewallet.Core.Implementations;

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
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ILogin, Login>();
            services.AddTransient<IRegister, Register>();
            services.AddScoped<ITransactionRepository, TransactionsRepository>();
            services.AddScoped<IAuthService, AuthService>();


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();

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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }
    }
}
