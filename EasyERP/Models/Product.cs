using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int Type { get;  set; }

        public String Name { get; set; }

        public string ImageName { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public bool Availability { get; set; }

        public struct Types
        {
            public const int CHAIR = 1; //fotel
            public const int SOFA = 2; //kanapa
        }
    }
}
