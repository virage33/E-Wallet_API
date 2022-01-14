using AutoMapper;
using Ewallet.Commons;
using Ewallet.Core.Implementations;
using Ewallet.Core.Interfaces;
using Ewallet.Core.JWT.Implementations;
using Ewallet.Core.JWT.Interfaces;
using Ewallet.DataAccess.EntityFramework;
using Ewallet.DataAccess.EntityFramework.Implementations;
using Ewallet.DataAccess.EntityFramework.Interfaces;
using Ewallet.Models;
using Ewallet.Models.DTO;
using EwalletApi.Controllers;
using EwalletApi.Models.AccountModels;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Ewallet.Test
{
    public class AuthServiceUnitTestWithFakeItEasy
    {
        public ICurrencyService currencyService = A.Fake<ICurrencyService>();
        public IWalletRepository walletRepo = A.Fake<IWalletRepository>();
        public IWalletServices WalletServices = A.Fake<IWalletServices>();
        public IUserRepository UserRepository = A.Fake<IUserRepository>();
        public IJwtService JwtService = A.Fake<IJwtService>();
       
        public IMapper autoMapper = A.Fake<IMapper>();
        public ICurrencyConversionService currencyConversion = A.Fake<ICurrencyConversionService>();
        public IUnitOfWork unit = A.Fake<IUnitOfWork>();

      

        [Fact]
        public async Task LoginAuthSuccessfulTest()
        {
            //arrange
            var testData = new LoginDTO { Email = "suleiman.sani1@gmail.com", Password = "1234567" };
            var AuthService = new AuthService(UserRepository,JwtService,WalletServices);
            var result = A.Fake<AppUser>();
            var roles = A.CollectionOfDummy<string>(1);
            string token = ""; 
            
            A.CallTo(() => UserRepository.GetUserByEmail(testData.Email)).Returns(Task.FromResult(result));
            result.IsActive = true;
            result.password = testData.Password;
            result.Email = testData.Email;
            
            A.CallTo(() => UserRepository.GetUserRoles(result)).Returns(Task.FromResult(roles));
            roles[0]="admin";

            A.CallTo(() => JwtService.GenerateToken(result, roles)).Returns(token="hghjb");
            

            
            //act
            var resp = await AuthService.Login(testData);
            //assert
            Assert.NotNull(resp.Data);
        }

        [Fact]
        public async Task LoginAuthWrongPasswordTest()
        {
            //arrange
            var testData = new LoginDTO { Email = "suleiman.sani1@gmail.com", Password = "1234567" };
            var AuthService = new AuthService(UserRepository, JwtService, WalletServices);
            var result = A.Fake<AppUser>();

            A.CallTo(() => UserRepository.GetUserByEmail(testData.Email)).Returns(Task.FromResult(result));
            result.IsActive = true;
            result.password = "sdsd";
            result.Email = testData.Email;

            //act
            var resp = await AuthService.Login(testData);
            //assert
            Assert.Equal("Wrong password",resp.Message);
        }

        [Fact]
        public async Task LoginAuthWrongEmailTest()
        {
            //arrange
            var testData = new LoginDTO { Email = "suleiman2.sani1@gmail.com", Password = "1234567" };
            var AuthService = new AuthService(UserRepository, JwtService, WalletServices);
            var result = A.Fake<AppUser>();

            A.CallTo(() => UserRepository.GetUserByEmail(testData.Email)).Returns(Task.FromResult(result=null));
            

            //act
            var resp = await AuthService.Login(testData);
            //assert
            Assert.Equal("Wrong email or Password",resp.Message);
        }

        [Fact]
        public async Task LoginAuthDeactivatedUserTest()
        {
            //arrange
            var testData = new LoginDTO { Email = "suleiman.sani1@gmail.com", Password = "1234567" };
            var AuthService = new AuthService(UserRepository, JwtService, WalletServices);
            var result = A.Fake<AppUser>();

            A.CallTo(() => UserRepository.GetUserByEmail(testData.Email)).Returns(Task.FromResult(result));
            result.password = testData.Password;
            result.Email = testData.Email; 

            //act
            var resp = await AuthService.Login(testData);
            //assert
            Assert.Equal("Deactivated Account", resp.Message);
        }
    }
}
