using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GLOWING_FinalProject.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength: 600)]
        public string Description { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }
    }
}
