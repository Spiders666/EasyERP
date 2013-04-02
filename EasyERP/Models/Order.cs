﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Customer Customer { get; set; }
        public ICollection<Setting> Settings { get; set; }
    }
}
