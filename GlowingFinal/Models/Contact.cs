using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string Icon { get; set; }

        [MaxLength(10)]
        public string Type { get; set; }
    }
}
