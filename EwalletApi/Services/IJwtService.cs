using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.UI.Services
{
    public interface IJwtService
    {
        public string GenerateToken();
    }
}
