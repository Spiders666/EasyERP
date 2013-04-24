using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public MaterialType MaterialType { get; set; }

        [Required]
        public String MaterialName { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public Order Order { get; set; }
    }
}
