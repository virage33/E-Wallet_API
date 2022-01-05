using Ewallet.Core.JWT.Interfaces;
using Ewallet.Models;
using EwalletApi.Models;
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
        public string GenerateToken(AppUser user, IList<string>roles)
        {
            //add claims
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, $"{user.Email}"),
            };

            //add roles to claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
                    
            
            //set secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTKey"]));

            //define security token descriptor
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(key, algorithm: SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddDays(1)
            };

          

            //create token
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDiscriptor);
            var t = handler.WriteToken(token);
            return t;

        }
    }
}
