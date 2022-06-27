using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class InvoiceNo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int iNumber { get; set; }
    }

}
