using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
  
        public string Image { get; set; }
        public string DetailImage { get; set; }

        public DateTime Date { get; set; }

        [NotMapped]
        [Required]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        [Required]
        public IFormFile DetailImageFile { get; set; }
  
        public string UserId { get; set; }
        public User User { get; set; }
        public List<BlogCategoryToBlog> BlogCategoryToBlogs { get; set; }
        public List<Comment> Comments { get; set; }


    }
}
