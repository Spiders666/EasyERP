﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Part
    {
        public const int TYPE_UPHOLSTERY = 1; //tapicerka
        public const int TYPE_FILL = 2; //wypełnienie

        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int PhotoId { get; set; }

        public int Type { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public bool Availability { get; set; }
    }
}
