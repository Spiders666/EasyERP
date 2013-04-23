using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyERP.Models;
using EasyERP.Helpers;

namespace EasyERP.Areas.Admin.ViewModels
{
    public class OrderDetailsModel
    {
        public Order Order { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal OrderItemsTotalPrice
        {
            get { return OrderItems.Sum(o => o.Price); }
        }

        public decimal TotalPrice
        {
            get { return Order.ProductPrice + OrderItemsTotalPrice; }
        }

        public OrderDetailsModel(Order Order, List<OrderItem> OrderItems)
        {
            this.Order = Order;
            this.OrderItems = OrderItems;
        }
    }
}
