using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Part
    {
        public int Id { get; set; }
        public int SuppierId { get; set; }
        public int TypetId { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual Type Type { get; set; }
        public ICollection<Setting> Settings { get; set; }
    }
}
