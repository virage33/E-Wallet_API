using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EwalletApi.UI.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration configuration;
        public JwtService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string GenerateToken()
        {
            var claims = new List<Claim>();
            var claim = new Claim(type: ClaimTypes.NameIdentifier, value: "MyClaim");
            claims.Add(claim);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTKey"]));

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(key, algorithm: SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddDays(1)
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDiscriptor);
            var t = handler.WriteToken(token);
            return t;

        }
    }
}
