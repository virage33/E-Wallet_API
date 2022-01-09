using Ewallet.Core.Implementations;
using Ewallet.Core.Interfaces;
using Ewallet.Core.JWT.Implementations;
using Ewallet.Core.JWT.Interfaces;
using Ewallet.DataAccess.EntityFramework.Interfaces;
using Ewallet.Models;
using Ewallet.Models.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using Xunit;

namespace Ewallet.Test
{
    public class AuthServiceUnitTestWithMoq
    {
        private readonly Mock<IUserRepository> UserRepository = new Mock<IUserRepository>();
        private readonly Mock<IJwtService> JwtService = new Mock<IJwtService>();
        private readonly Mock<IWalletServices> WalletServices = new Mock<IWalletServices>();
        private readonly Mock<IConfiguration> configuration = new Mock<IConfiguration>();
        private readonly Mock<System.Text.Encoding> encode = new Mock<System.Text.Encoding>();
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;
        public AuthServiceUnitTestWithMoq()
        {
            _jwtService = new JwtService(configuration.Object);
            _authService = new AuthService(UserRepository.Object, JwtService.Object, WalletServices.Object);
        }

        [Fact]
        public async Task GenerateJWTTokenTest()
        {
            //arrange
            var User = new AppUser();
            User.Id = Guid.NewGuid().ToString();
            User.Email = "suleiman.sani1@gmail.com";
            User.UserName = User.Email;
            User.FirstName = "susu";
            User.LastName = "fafa";
            IList<string> roles = new List<string> {"admin"};
            var k = new byte[256];
            
            encode.Setup(x => x.GetBytes("ghfhjn")).Returns(k);
            var key = new SymmetricSecurityKey(k);

             
            encode.Verify(x => x.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJOYW1lIjoiTXlDbGFpbSIsIm5iZiI6MTYzNzcxMDk5OCwiZXhwIjoxNjM3Nzk3Mzk4LCJpYXQiOjE2Mzc3MTA5OTh9.vQMGazi3m6nBnYvLbebyeylVPHgvXxlE6Mqv0bfLGLc"));
            //act
            var response =  _jwtService.GenerateToken(User,roles);
            //assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task LoginAuthWrongEmailTest()
        {
            //arrange
            var testData = new LoginDTO { Email = "suleiman.sani1@gmail.com", Password = "1234567" };
            var user = new AppUser { Email = testData.Email, FirstName="susu",LastName="fafa",UserName=testData.Email};
            UserRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(()=>null);
            //act
            var auth = await _authService.Login(testData);
            //Assert
            Assert.Equal("Wrong email or Password", auth);

        }
    }
}
