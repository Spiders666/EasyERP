using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace EasyERP.Models
{
    public class PartCategory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public string Name { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public ICollection<Part> Parts { get; set; }
    }
}
