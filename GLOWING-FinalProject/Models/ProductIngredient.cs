using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GLOWING_FinalProject.Models
{
    public class ProductIngredient
    {
        public int Id { get; set; }
        public string CSS { get; set; }
        public string INCI { get; set; }
        public string Composition { get; set; }
        public string Appearance { get; set; }
        public string Solubility { get; set; }
        public string Storage { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
