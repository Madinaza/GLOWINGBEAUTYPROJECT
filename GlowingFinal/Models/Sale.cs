using GlowingFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        public string InvoiceNo { get; set; }

        public DateTime SaleDate { get; set; }

        [ForeignKey("User")]
        public string EndUserId { get; set; }
        public User EndUser { get; set; }

        [ForeignKey("UnregisteredCustomer")]
        public int? UnregisteredCustomerId { get; set; }
        public UnregisteredCustomer UnregisteredCustomer { get; set; }

        public List<SaleItem> SaleItems { get; set; }

        public bool hideToClien { get; set; }

        public bool isReaded { get; set; }

        public bool isHide { get; set; }
    }
}
