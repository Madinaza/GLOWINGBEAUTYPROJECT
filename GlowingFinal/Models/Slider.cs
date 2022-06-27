using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlowingFinal.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 40)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength:20)]
        public string SignatureTitle { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Description { get; set; }

        public string Image { get; set; }
        public int Order { get; set; }
        [NotMapped]
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
