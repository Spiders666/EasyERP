using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyERP.Models;

namespace EasyERP.Areas.Admin.ViewModels
{
    public class OrderDetails
    {
        public Order Order { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public OrderDetails(Order Order, List<OrderItem> OrderItems)
        {
            this.Order = Order;
            this.OrderItems = OrderItems;
        }
    }
}
