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
            var products = from p in db.Products
                           where p.Availability == true
                           orderby p.Id descending
                           select p;

            var productslist = db.Products.ToList(); //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

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
                                where a.TypeId == category &&
                                a.Availability == true
                                orderby a.Name
                                select a;

            var cat = queryproducts.ToList();

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = cat.ToPagedList(pageNumber, 9); // will only contain 9 products max because of the pageSize

            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }
        //
        // GET: /Product/Details/1/1
        
        public ActionResult Details(int id)
        {
            SessionSettings sessionSettings = SessionSettings.GetInstance(this.HttpContext);

            var GetTypeQuery = from m in db.Products
                               where m.Id == id &&
                               m.Availability == true
                               select m.TypeId;
            var type = GetTypeQuery.FirstOrDefault();
            if (type == 0)
            {
                return HttpNotFound();
            }
            var ConfQuery = from m in db.Configurations.Include(p => p.MaterialType)
                            where m.ProductTypeId == type
                            select m;
            ViewBag.ConfigurationList = ConfQuery.ToList();
            ViewBag.ReturnUrl = id;
            ViewBag.NotSet = 0;
            ViewBag.HasConfigurator = 0;

            List<Material> listMaterial = new List<Material>();
            foreach (var i in ConfQuery.ToList()){
                ViewBag.HasConfigurator = 1;
                int session = sessionSettings.GetMaterialId(i.MaterialTypeId);
                var query = from m in db.Materials.Include(p => p.Type)
                            where m.Id == session
                            select m;
                var GetQuery = query.FirstOrDefault();
                if (GetQuery == null)
                {
                    ViewBag.NotSet = 1;
                    listMaterial.Add(new Material());
                }
                else
                {
                    listMaterial.Add(GetQuery);
                }
            }

            decimal sum = 0.00m;
            foreach (var i in listMaterial){
                var QueryMaterial = from m in db.Materials
                            where m.Id == i.Id
                            select m.Price;
                var GetQueryMaterial = QueryMaterial.FirstOrDefault();
                sum = sum + GetQueryMaterial;
            }
            var QueryProduct = from m in db.Products
                        where m.Id == id
                        select m.Price;
            var GetQueryProduct = QueryProduct.FirstOrDefault();
            sum = sum + GetQueryProduct;
            ViewBag.Chosen = listMaterial;
            ViewBag.Sum = String.Format("{0:0.00} zł", sum);
            ViewBag.Category = type;

            Product product = db.Products.Find(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        //
        // POST: /Product/Details/1

        [HttpPost]
        public ActionResult Details(Product product)
        {
            
            //check if customer is Authenticated
            if (!Request.IsAuthenticated || User.IsInRole("Administrator"))
            {
                return RedirectToAction("Login", "Account");
            }
            //check if customer has all data set up
            if (Helpers.AccountHelpers.GetCustomerId() == 0)
            {
                return RedirectToAction("Register2", "Account");
            }
            SessionSettings sessionSettings = SessionSettings.GetInstance(this.HttpContext);

            var ProductQuery = from m in db.Products
                               where m.Id == product.Id
                               select m;
            var GetProduct = ProductQuery.FirstOrDefault();

            if (GetProduct == null)
            {
                return HttpNotFound();
            }

            if (product.TypeId == 0)
            {
                return HttpNotFound();
            }
            var ProductTypeId = from m in db.ProductTypes
                                   where m.Id == product.TypeId
                                   select m.Name;
            var GetProductTypeId = ProductTypeId.FirstOrDefault();

            var CustomerId = Helpers.AccountHelpers.GetCustomerId();
            var ProdQuery = from m in db.Configurations.Include(p => p.ProductType)
                            where m.ProductTypeId == GetProduct.TypeId
                            select m;
            var GetConfQuery =  ProdQuery.FirstOrDefault();

            // Adding order to database
            Order order = new Order();
            order.CustomerId = CustomerId;
            order.ProductTypeName = GetProductTypeId;
            order.ProductName = GetProduct.Name;
            order.ProductPrice = GetProduct.Price;
            order.CreatedAt = DateTime.Now;
            db.Orders.Add(order);
            db.SaveChanges();

            // Adding each configuration to order
            var ConfQuery = from m in db.Configurations.Include(p => p.MaterialType)
                            where m.ProductTypeId == GetProduct.TypeId
                            select m;
            if (GetConfQuery != null)
            {
                foreach (var i in ConfQuery.ToList())
                {
                    var MaterialId = i.MaterialTypeId;
                    var GetSessionSetting = sessionSettings.GetMaterialId(MaterialId);
                    var MaterialsQuery = from m in db.Materials.Include(p => p.Type)
                                         where m.Id == GetSessionSetting
                                         select m;
                    var GetMaterialsQuery = MaterialsQuery.FirstOrDefault();

                    OrderItem orderitem = new OrderItem();

                    orderitem.MaterialName = GetMaterialsQuery.Name;
                    orderitem.MaterialTypeName = GetMaterialsQuery.Type.Name;
                    orderitem.Order = order;
                    orderitem.OrderId = order.Id;
                    orderitem.Price = GetMaterialsQuery.Price;
                    db.OrderItems.Add(orderitem);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Cart", "Products");
        }
        
        //
        // GET: /Products/Cart

        public ActionResult Cart()
        {
            var CustomerId = Helpers.AccountHelpers.GetCustomerId();
            var orders = from o in db.Orders.Include(o => o.Customer).Include(o => o.OrderItems)
                         where o.CustomerId == CustomerId
                         orderby o.Id descending
                         select o;
            var GetOrders = orders.ToList();

            return View(GetOrders);
        }

        //
        // Post: /Products/Cart

        [HttpPost]
        public ActionResult Cart(int id = 0, string name = "")
        {
            var query = from q in db.Orders
                        where q.Id == id
                        select q;
            var GetQuery = query.FirstOrDefault();
            if (GetQuery == null)
            {
                return HttpNotFound();
            }
            if (name == "order")
            {
                GetQuery.State = OrderState.Pending;
                db.Entry(GetQuery).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
            if (name == "cancel")
            {
                GetQuery.State = OrderState.Canceled;
                db.Entry(GetQuery).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Cart");
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
                where m.Type.Id == type &&
                m.Availability == true
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
