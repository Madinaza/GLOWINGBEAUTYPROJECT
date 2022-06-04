using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GLOWING_FinalProject.Models
{
    public class EndUser: IdentityUser
    {
        

        [MaxLength(40), Required]
        public string FullName { get; set; }

        [MaxLength(100), Required]
        public string ShippingAddress { get; set; }

        [MaxLength(100), Required]
        public string BillingAddress { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(255)]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

    


        [MaxLength(500)]
        public string ResetPasswordCode { get; set; }

     

        public string UserCart { get; set; }

        public string UserFavourite { get; set; }


    }
}
