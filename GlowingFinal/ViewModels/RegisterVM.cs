﻿using GlowingFinal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Fullname { get; set; }
        [Required, MaxLength(30)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }


        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
