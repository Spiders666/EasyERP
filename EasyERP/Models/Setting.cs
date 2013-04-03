using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public int OrderItemId { get; set; }
        public int PartId { get; set; }

        public virtual OrderItem OrderItem { get; set; }
        public virtual Part Part { get; set; }
    }
}
