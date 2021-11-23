using EwalletApi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.Services.AuthService
{
    interface IRegister
    {
        void RegisterUser(RegisterDTO data);
    }
}
