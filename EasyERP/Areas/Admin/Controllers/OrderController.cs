using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;
using EasyERP.Areas.Admin.ViewModels;
using System.Dynamic;

namespace EasyERP.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            var orders = from o in db.Orders.Include(o => o.Customer)
                         select o;

            return View(orders);
        }

        public ActionResult Details(int id = 0)
        {
            var query = from o in db.Orders.Include(o => o.Customer).Include(o => o.OrderItems)
                        where o.Id == id
                        select o;
            var order = query.FirstOrDefault();

            if (order == null)
            {
                return HttpNotFound();
            }

            decimal totalPrice = order.OrderItems.Sum(o => o.Price);

            return View(new OrderDetailsViewModel(order, totalPrice));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
