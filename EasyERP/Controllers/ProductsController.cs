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
using WebMatrix.WebData;

namespace EasyERP.Controllers
{
    public class ProductsController : Controller
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
        // GET: /Product/List/1/1

        public ActionResult List(int? page, int? category)
        {
            ProductType productType = (Enum.IsDefined(typeof(ProductType), category) ? (ProductType)category : ProductType.ARMCHAIR);

            var queryproducts = 
                from a in db.Products
                where a.Type == productType
                orderby a.Name
                select a;

            var cat = queryproducts.ToList();

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = cat.ToPagedList(pageNumber, 9); // will only contain 9 products max because of the pageSize

            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }
        //
        // GET: /Product/Details/1

        public ActionResult Details(int id)
        {
            Configurator configurator = Configurator.GetInstance(this.HttpContext);

            if (!configurator.isMaterialExists(MaterialType.FILL))
                ViewBag.MaterialFill = "nie wybrano!";
            if (!configurator.isMaterialExists(MaterialType.UPHOLSTERY))
                ViewBag.MaterialUp = "nie wybrano!";

            if (configurator.isMaterialExists(MaterialType.FILL))
            {
                int idfill = configurator.GetMaterialId(MaterialType.FILL);
                var queryfill = from m in db.Materials
                                where m.Id == idfill
                                select m;
                var materialfill = queryfill.FirstOrDefault();
                if (materialfill == null)
                    return HttpNotFound();
                ViewBag.MaterialFill = idfill.ToString();
            }
            if (configurator.isMaterialExists(MaterialType.UPHOLSTERY))
            {
                int idup = configurator.GetMaterialId(MaterialType.UPHOLSTERY);
                var queryup = from m in db.Materials
                              where m.Id == idup
                              select m;
                var materialup = queryup.FirstOrDefault();
                if (materialup == null)
                    return HttpNotFound();
                ViewBag.MaterialUp = idup.ToString();
            }

            Product product = db.Products.Find(id);
            if (product == null)
                return HttpNotFound();
            ViewBag.ReturnUrl = id;
            return View(product);
        }

        //
        // POST: /Product/Details/1

        [HttpPost]
        public ActionResult Details(Product product, Order order)
        {
            Configurator configurator = Configurator.GetInstance(this.HttpContext);

            var ProductId = product.Id;

            if (!configurator.isMaterialExists(MaterialType.FILL))
                return RedirectToAction("Details", "Products", new {id = ProductId});
            if (!configurator.isMaterialExists(MaterialType.UPHOLSTERY))
                return RedirectToAction("Details", "Products", new {id = ProductId});
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login","Account");
            if (Helpers.AccountHelpers.GetCustomerId() == 0)
                return RedirectToAction("Details", "Products", new {id = ProductId});

            order.CustomerId = Helpers.AccountHelpers.GetCustomerId();
            order.ProductName = product.Name;
            order.ProductPrice = product.Price;
            order.CreatedAt = DateTime.Now;
            db.Orders.Add(order);
            db.SaveChanges();

            //add order save
            //
            return RedirectToAction("Cart", "Products");
        }

        //
        // GET: /Products/Cart

        public ActionResult Cart()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DatabaseContext", "UserProfile", "UserId", "UserName", autoCreateTables: false);

            var CustomerId = Helpers.AccountHelpers.GetCustomerId();
            var orders = from o in db.Orders.Include(o => o.Customer)
                         where o.CustomerId == CustomerId
                         select o;

            return View(orders);
        }

        public ActionResult Set(int type = 1, int id = 1, int returnurl = 1)
        {
            Configurator configurator = Configurator.GetInstance(this.HttpContext);

            var query = from m in db.Materials
                        where m.Id == id
                        select m;

            var material = query.FirstOrDefault();

            if (material == null)
            {
                return HttpNotFound();
            }
            if (type == 1)
                configurator.SetMaterial(material.Type, material.Id);
            else
                configurator.SetMaterial(material.Type, material.Id);

            ViewBag.MaterialId = material.Id;
            ViewBag.MaterialType = material.Type.ToString();
            ViewBag.ReturnUrl = returnurl;
            return View();
        }

        public ActionResult MaterialList(int? page, int? type, int returnurl)
        {
            if (page == null | type == null)
                return RedirectToAction("List");

            MaterialType materialType = (Enum.IsDefined(typeof(MaterialType), type) ? (MaterialType)type : MaterialType.FILL);

            var query =
                from a in db.Materials
                where a.Type == materialType
                orderby a.Name
                select a;

            var pageNumber = page ?? 1; 
            var onePageOfProducts = query.ToPagedList(pageNumber, 9); 

            ViewBag.OnePageOfProducts = onePageOfProducts;
            ViewBag.ReturnUrl = returnurl;
            ViewBag.Type = type;
            return View();
        }
    }
}
