using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public enum ProductType
    {
        CHAIR = 1, //fotel
        SOFA = 2 //kanapa
    }

    public class Product
    {
        public int Id { get; set; }

        public ProductType Type { get; set; }

        public String Name { get; set; }

        public string ImageName { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public bool Availability { get; set; }
    }
}
