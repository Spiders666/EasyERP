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
            var queryproducts = from a in db.Products
                                where a.TypeId == category
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
        
        public ActionResult Details(int id, int type)
        {
            SessionSettings sessionSettings = SessionSettings.GetInstance(this.HttpContext);

            var ConfQuery = from m in db.Configurations.Include(p => p.MaterialType)
                            where m.ProductTypeId == type
                            select m;
            ViewBag.Test = ConfQuery.ToList().Count();
            ViewBag.ConfigurationList = ConfQuery.ToList();
            ViewBag.ReturnUrl = id;
            ViewBag.MaterialId = sessionSettings.GetMaterialId(id);

            List<int> listId = new List<int>();

            foreach (var i in ConfQuery.ToList()){
                var MaterialId = i.MaterialTypeId;
                listId.Add(sessionSettings.GetMaterialId(MaterialId));
            }
            ViewBag.Chosen = listId;
            ViewBag.Category = type;

            Product product = db.Products.Find(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        //
        // POST: /Product/Details/1

        [HttpPost]
        public ActionResult Details(Product product, Order order, OrderItem orderitem)
        {
            SessionSettings sessionSettings = SessionSettings.GetInstance(this.HttpContext);
            
            var CustomerId = Helpers.AccountHelpers.GetCustomerId();
            var ProdQuery = from m in db.Configurations.Include(p => p.ProductType)
                            where m.ProductTypeId == product.TypeId
                            select m;

            var GetConfQuery =  ProdQuery.FirstOrDefault();

            // Adding order to database
            order.CustomerId = CustomerId;
            order.ProductTypeName = GetConfQuery.ProductType.Name;
            order.ProductName = product.Name;
            order.ProductPrice = product.Price;
            order.CreatedAt = DateTime.Now;
            db.Orders.Add(order);
            db.SaveChanges();

            var ConfQuery = from m in db.Configurations.Include(p => p.MaterialType)
                            where m.ProductTypeId == product.TypeId
                            select m;
            // Adding each configuration to order
            foreach (var i in ConfQuery.ToList())
            {
                var MaterialId = i.MaterialTypeId;
                var GetSessionSetting = sessionSettings.GetMaterialId(MaterialId);
                var MaterialsQuery = from m in db.Materials.Include(p => p.Type)
                                     where m.Id == GetSessionSetting
                                     select m;
                var GetMaterialsQuery = MaterialsQuery.FirstOrDefault();

                orderitem.MaterialName = GetMaterialsQuery.Name;
                orderitem.MaterialTypeName = GetMaterialsQuery.Type.Name;
                orderitem.Order = order;
                orderitem.OrderId = order.Id;
                orderitem.Price = GetMaterialsQuery.Price;
                db.OrderItems.Add(orderitem);
                db.SaveChanges();
            }
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

        public ActionResult Set(int id = 1,int category = 1, int returnurl = 1)
        {
            SessionSettings sessionSettings = SessionSettings.GetInstance(this.HttpContext);

            var query = from q in db.Materials.Include(m => m.Type)
                        where q.Id == id
                        select q;

            var material = query.FirstOrDefault();

            if (material == null)
            {
                return HttpNotFound();
            }
            sessionSettings.SetMaterial(material.Type.Id, material.Id);
            ViewBag.Category = category;
            ViewBag.ReturnUrl = returnurl;
            return View();
        }
        public ActionResult MaterialList(int? page, int? type, int category, int returnurl)
        {
            if (page == null | type == null)
                return RedirectToAction("List");

            var query =
                from m in db.Materials
                where m.Type.Id == type
                orderby m.Name
                select m;

            var pageNumber = page ?? 1; 
            var onePageOfProducts = query.ToPagedList(pageNumber, 9); 

            ViewBag.OnePageOfProducts = onePageOfProducts;
            ViewBag.ReturnUrl = returnurl;
            ViewBag.Category = category;
            return View();
        }
    }
}
