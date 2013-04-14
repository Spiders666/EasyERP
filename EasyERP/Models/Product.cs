using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Product
    {
        public const int TYPE_CHAIR = 1; //fotel
        public const int TYPE_SOFA = 2; //kanapa

        public int Id { get; set; }
        public int PhotoId { get; set; }

        public int Type { get; set; }

        public String Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public bool Availability { get; set; }
    }
}
