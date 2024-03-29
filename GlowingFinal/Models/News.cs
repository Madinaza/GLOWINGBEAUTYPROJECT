﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class News
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Title { get; set; }
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string ButtonTitle { get; set; }


        [NotMapped]
        [Required]
        public IFormFile ImageFile { get; set; }

    }
}
