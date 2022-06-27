using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class VmForgotPassword:VmBase
    {
        [Required, MaxLength(250)]
        public string Email { get; set; }
    }
}
