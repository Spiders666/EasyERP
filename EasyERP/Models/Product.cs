using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public enum ProductType
    {
        ARMCHAIR = 1, //fotel
        SOFA = 2, //sofa
        BED = 3 //łóżko
    }

    public class Product
    {
        public int Id { get; set; }

        [Required]
        public ProductType Type { get; set; }

        [Required]
        public String Name { get; set; }

        public string ImageName { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public bool Availability { get; set; }

        [Timestamp]
        public byte[] CurrentVersion { get; set; }
    }
}
