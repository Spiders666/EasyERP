using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;
using System.Dynamic;
using EasyERP.Helpers;

namespace EasyERP.Areas.Admin.Controllers
{
    [Authorize(Roles = UserRole.Administrator)]
    public class OrderController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            var query = from q in db.Orders.Include(o => o.Customer)
                        orderby q.Id descending
                        select q;

            var orders = query.ToList();

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

            decimal totalPrice = order.ProductPrice + order.OrderItems.Sum(o => o.Price);
            ViewBag.TotalPrice = totalPrice;
            return View(order);
        }

        [HttpPost]
        public ActionResult Details(int id = 0, OrderState state = (OrderState)(-1))
        {
            if (!Enum.IsDefined(typeof(OrderState), state))
            {
                return HttpNotFound();
            }

            var query = from o in db.Orders.Include(o => o.Customer).Include(o => o.OrderItems)
                        where o.Id == id
                        select o;

            var order = query.FirstOrDefault();

            if (order == null)
            {
                return HttpNotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    order.State = state;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();

                    FlashMessageHelper.SetMessage(
                        this,
                        HttpContext.GetGlobalResourceObject(
                            "Resources",
                            "AdminControllerEditSuccess").ToString(),
                        FlashMessageHelper.TypeOption.Success
                    );
                    return RedirectToAction("Index");
                }

                FlashMessageHelper.SetMessage(
                    this,
                    HttpContext.GetGlobalResourceObject(
                        "Resources",
                        "AdminControllerEditError").ToString(),
                    FlashMessageHelper.TypeOption.Error
                );
            }
            catch (Exception)
            {
                FlashMessageHelper.SetMessage(
                    this,
                    HttpContext.GetGlobalResourceObject(
                        "Resources",
                        "AdminControllerEditWarning").ToString(),
                    FlashMessageHelper.TypeOption.Warning
                );
            }

            decimal totalPrice = order.ProductPrice + order.OrderItems.Sum(o => o.Price);
            ViewBag.TotalPrice = totalPrice;
            return View(order);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
