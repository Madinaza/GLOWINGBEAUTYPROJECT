using GlowingFinal.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Areas.Dashboard.ViewModels
{
    public class ProductPostViewModel
    {

        //post
        [Required]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
        public int Price { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Desc { get; set; }
        [Required]
        [StringLength(maximumLength: 18)]
        public string SKUCode { get; set; }
       
      
        public IFormFile MainImage { get; set; }
        public IFormFile[] Images { get; set; }
        public int? CampaignId { get; set; }
        public List<int> CategoryIds { get; set; }


        //get
        public List<ProductCategory> Categories { get; set; }
        public List<Campaign> Campaigns { get; set; }
    }
}
