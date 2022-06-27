using GlowingFinal.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class VmProfile:VmBase
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(250)]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Password { get; set; }

        [Required, MaxLength(250)]
        public string State { get; set; }

        [Required, MaxLength(20)]
        public string Phone { get; set; }

        [Required, MaxLength(10)]
        public string Zipcode { get; set; }

       
        public string Image { get; set; }
        public User Users { get; set; }



        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
