using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class MaterialType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Timestamp]
        public byte[] CurrentVersion { get; set; }
    }
}
