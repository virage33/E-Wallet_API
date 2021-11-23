﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletApi.Models.DTO
{
    public class LoginDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
 
        [Required]
        public string Password { get; set; }
    }
}
