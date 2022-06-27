using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class CommentPostVM
    {
        [Required,MaxLength(500)]
        public string Desc { get; set; }
    }
}
