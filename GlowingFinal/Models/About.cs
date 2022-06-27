using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class About
    {

      
        public int Id { get; set; }
        [MaxLength(100)]
        public string Heading { get; set; }

        public string SliderImage { get; set; }
        [NotMapped]
        public IFormFile SliderImageFile { get; set; }
        public string MainTitle { get; set; }
        public string MainDesc { get; set; }
        public string Image1 { get; set; }

        [NotMapped]
        public IFormFile ImageFile1 { get; set; }


        public string Image2 { get; set; }

        [NotMapped]
        public IFormFile ImageFile2 { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }

        public string Desc1 { get; set; }
        public string Desc2 { get; set; }



    }
}
