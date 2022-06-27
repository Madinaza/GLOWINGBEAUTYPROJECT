using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class User:IdentityUser
    {
        
        public string? Name { get; set; }

      
        public string? Surname { get; set; }
        [Required, MaxLength(50)]
        public string FullName { get; set; }


        [NotMapped]
        public string RoleId { get; set; }

        [NotMapped]
        public List<SelectListItem> Roles { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [MaxLength(250)]
        public string State { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(250)]
        public string ZipCode { get; set; }

        //public List<Sale> Sales { get; set; }


        [MaxLength(255)]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<Comment> Comments { get; set; }

        public List<ProductComment> ProductComments { get; set; }



        [MaxLength(100)]
        public string? ShippingAddress { get; set; }

        [MaxLength(100)]
        public string? BillingAddress { get; set; }


        public string UserFavourite { get; set; }

        public string UserCart { get; set; }

    }
}
