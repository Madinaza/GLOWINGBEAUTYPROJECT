using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(3000), Required]
        public string FooterDesc { get; set; }

        [MaxLength(20), Required]
        public string Phone { get; set; }

        [MaxLength(200), Required]
        public string Address { get; set; }

        [MaxLength(25), Required]
        public string Copyright { get; set; }

        [MaxLength(250), Required]
        public string CopyrightLink { get; set; }

        [MaxLength(255)]
        public string Logo { get; set; }

        [NotMapped]
        public IFormFile LogoFile { get; set; }
    }
}
