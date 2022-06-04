using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GLOWING_FinalProject.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string CountryName { get; set; }

        public List<EndUser> EndUsers { get; set; }
    }
}
