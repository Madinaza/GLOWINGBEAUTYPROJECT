using GlowingFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class BlogVM
    {
        public Blog Blog { get; set; }
        public CommentPostVM Comment { get; set; }

    }
}
