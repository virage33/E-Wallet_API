using Ewallet.Core.JWT.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ewallet.Core.JWT.Implementations
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
            var adminClaim = new Claim(type: "role", value: "admin");
            claims.Add(claim);
            claims.Add(adminClaim);

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
