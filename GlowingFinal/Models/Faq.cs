using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class Faq
    {

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        [MaxLength(250)]
        public string Question { get; set; }

        [MaxLength(2000)]
        public string Answer { get; set; }

        public string Title2 { get; set; }

        [MaxLength(250)]
        public string Question2 { get; set; }

        [MaxLength(2000)]
        public string Answer2 { get; set; }

        public string Title3 { get; set; }

        [MaxLength(250)]
        public string Question3 { get; set; }

        [MaxLength(2000)]
        public string Answer3 { get; set; }


    }
}
