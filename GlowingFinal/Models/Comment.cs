using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class Comment
    {
        public string Description { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime DeletedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
