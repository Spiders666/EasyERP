using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Material
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }

        public int Type { get; set; }

        public string Name { get; set; }

        public string ImageName { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public bool Availability { get; set; }

        public struct Types
        {
            public const int UPHOLSTERY = 1; //obicie
            public const int FILL = 2; //wypełnienie
        }
    }
}
