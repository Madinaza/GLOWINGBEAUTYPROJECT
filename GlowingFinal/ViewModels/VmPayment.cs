using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class VmPayment
    {

        [MaxLength(20), Required]
        public string FirstName { get; set; }

        [MaxLength(20), Required]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string CompanyName { get; set; }

        [MaxLength(30), Required]
        public string CountyName { get; set; }

        [MaxLength(30), Required]
        public string TownCity { get; set; }

        [MaxLength(100), Required]
        public string Address { get; set; }

        [MaxLength(10), Required]
        public string PostcodeZip { get; set; }

        [MaxLength(50)]
        public string Apartment { get; set; }

        [MaxLength(50), Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(25), Required]
        [Phone]
        public string Phone { get; set; }

        [MaxLength(1000)]
        public string OrderNotes { get; set; }
    }
}
