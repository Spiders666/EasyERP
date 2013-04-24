using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyERP.Models;
using EasyERP.Helpers;

namespace EasyERP.Areas.Admin.ViewModels
{
    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderDetailsViewModel(Order Order, decimal totalPrice)
        {
            this.Order = Order;
            this.TotalPrice = totalPrice;
        }
    }
}
