using EwalletApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ewallet.Core.JWT.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(UserModel user, List<string> roles);
    }
}
