﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public int ProductTypeId { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Description { get; set; }

        public string ImageName { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public bool Availability { get; set; }

        [Timestamp]
        public byte[] CurrentVersion { get; set; }

        public ProductType Type { get; set; }
    }
}
