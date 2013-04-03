using System;
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

        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public ICollection<Setting> Settings { get; set; }
    }
}
