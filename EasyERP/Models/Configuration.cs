using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Configuration
    {
        public int Id { get; set; }

        public int ProductTypeId { get; set; }
        public int MaterialTypeId { get; set; }

        public MaterialType MaterialType { get; set; }
        public ProductType ProductType { get; set; }
    }
}
