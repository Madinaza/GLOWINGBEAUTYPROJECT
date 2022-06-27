using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string CountryName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N3}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ShippingPrice { get; set; }

      
        

        public List<User> EndUsers { get; set; }
    }
}
