using EwalletApi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.Services.AuthService.Interfaces
{
    public interface ILogin
    {
        string LogIn(LoginDTO data); 
    }
}
