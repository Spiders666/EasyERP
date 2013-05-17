using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;
using PagedList;
using PagedList.Mvc;

namespace EasyERP.Controllers
{
    public class ProductController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //
        // GET: /Product/

        public ActionResult Index(int? page)
        {
            var products = db.Products.ToList(); //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = products.ToPagedList(pageNumber, 9); // will only contain 9 products max because of the pageSize

            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            var database = db.Materials;

            var queryfill = 
            from a in database
            where a.Type == MaterialType.FILL && a.Availability == true
            select a;

            var queryupholstery =
            from a in database
            where a.Type == MaterialType.UPHOLSTERY && a.Availability == true
            select a; 

            var fill = queryfill.ToList();
            var upholstery = queryupholstery.ToList();

            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.fill = fill;
            ViewBag.upholstery = upholstery;
            return View(product);
        }

        //
        // POST: /Product/Details/5

        [HttpPost]
        public ActionResult Details(Order order, OrderItem orderitem)
        {
            if (ModelState.IsValid)
            {
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}