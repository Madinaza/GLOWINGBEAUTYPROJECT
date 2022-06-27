using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50), Required]
        public string Email { get; set; }

        [MaxLength(25), Required]
        public string Phone { get; set; }

        [MaxLength(100), Required]
        public string Address { get; set; }

        [MaxLength(700)]
        public string? MapLink { get; set; }

        [MaxLength(555)]
        public string? BgImage { get; set; }

        [NotMapped]
        public IFormFile BgImageFile { get; set; }
    }
}
