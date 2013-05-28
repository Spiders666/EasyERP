﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Material
    {
        public int Id { get; set; }

        public int eqweqwId { get; set; }

        [Required]
        public int MaterialTypeId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageName { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public bool Availability { get; set; }

        [Timestamp]
        public byte[] CurrentVersion { get; set; }

        public MaterialType Type { get; set; }
        public Supplier Supplier { get; set; }
    }
}
