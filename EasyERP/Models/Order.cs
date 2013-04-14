using System;
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

        public String Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
