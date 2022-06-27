using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class Message
    {
        
        public int Id { get; set; }



        [MaxLength(80), Required]
        public string FullNAME { get; set; }

        [MaxLength(50), Required]
        public string Email { get; set; }

        [MaxLength(25)]
        public string Phone { get; set; }

        [MaxLength(2000), Required]
        public string Content { get; set; }

        public bool hasReaded { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
