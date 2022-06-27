using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class BankCarts
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Int64 CartNo { get; set; }

        [Required]
        public DateTime CardExpiry { get; set; }

        [Required]
        public int Cvc { get; set; }

        [MaxLength(60), Required]
        public string HolderName { get; set; }

        public decimal Balance { get; set; }
    }
}
