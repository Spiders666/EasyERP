﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public MaterialType MaterialType { get; set; }
        public String MaterialName { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual Order Order { get; set; }
    }
}
