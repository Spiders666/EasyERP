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

        [Required]
        [Display(Name = "Rodzaj")]
        public int TypeId { get; set; }

        [Required]
        [Display(Name = "Dostawca")]
        public int SupplierId { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        [StringLength(15, ErrorMessage = "{0} musi składać się z conajmniej {2} znaków.", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Zdjęcie")]
        public string ImageName { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Dostępność")]
        public bool Availability { get; set; }

        [Timestamp]
        public byte[] CurrentVersion { get; set; }

        public MaterialType Type { get; set; }
        public Supplier Supplier { get; set; }
    }
}
