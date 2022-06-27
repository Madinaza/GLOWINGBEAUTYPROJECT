using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class BlogCategoryToBlog
    {

        public int Id { get; set; }
        public int BlogId { get; set; }
        public int CategoryId { get; set; }
        public Blog Blog { get; set; }
        public BlogCategory Category { get; set; }
    }
}
