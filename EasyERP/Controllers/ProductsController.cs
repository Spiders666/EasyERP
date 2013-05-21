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
            var products = db.Products; //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

            var productchosen = new ProductType();

            if (category == 1)
                productchosen = ProductType.ARMCHAIR;
            else if (category == 2)
                productchosen = ProductType.BED;
            else if (category == 3)
                productchosen = ProductType.SOFA;
            else
                productchosen = ProductType.ARMCHAIR;

            var queryproducts =
            from a in products
            where a.Type == productchosen
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
            Configurator configuratorfill = Configurator.GetInstance(this.HttpContext);
            Configurator configuratorupholstery = Configurator.GetInstance(this.HttpContext);

            if (!configuratorfill.isMaterialExists(MaterialType.FILL))
                ViewBag.MaterialFill = "nie wybrano!";
            if (!configuratorupholstery.isMaterialExists(MaterialType.UPHOLSTERY))
                ViewBag.MaterialUp = "nie wybrano!";

            if (configuratorfill.isMaterialExists(MaterialType.FILL))
            {
                int idfill = configuratorfill.GetMaterialId(MaterialType.FILL);
                var queryfill = from m in db.Materials
                                where m.Id == idfill
                                select m;
                var materialfill = queryfill.FirstOrDefault();
                if (materialfill == null)
                    return HttpNotFound();
                ViewBag.MaterialFill = idfill.ToString();
            }
            if (configuratorupholstery.isMaterialExists(MaterialType.UPHOLSTERY))
            {
                int idup = configuratorupholstery.GetMaterialId(MaterialType.UPHOLSTERY);
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
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login","Account");
            order.CustomerId = WebSecurity.CurrentUserId;
            order.ProductName = product.Name;
            order.ProductPrice = 100.00m;
            order.CreatedAt = DateTime.Now;
            db.Orders.Add(order);
            db.SaveChanges();

            //add order save
            //
            return RedirectToAction("Index", "Admin");
        }

        //
        // GET: /Products/Cart

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Set(int type = 1, int id = 1, int returnurl = 1)
        {
            Configurator configuratorfill = Configurator.GetInstance(this.HttpContext);
            Configurator configuratorupholstery = Configurator.GetInstance(this.HttpContext);

            var query = from m in db.Materials
                        where m.Id == id
                        select m;

            var material = query.FirstOrDefault();

            if (material == null)
            {
                return HttpNotFound();
            }
            if (type == 1)
                configuratorfill.SetMaterial(material.Type, material.Id);
            else
                configuratorupholstery.SetMaterial(material.Type, material.Id);

            ViewBag.MaterialId = material.Id;
            ViewBag.MaterialType = material.Type.ToString();
            ViewBag.ReturnUrl = returnurl;
            return View();
        }


        public ActionResult Get()
        {
            Configurator configuratorfill = Configurator.GetInstance(this.HttpContext);
            Configurator configuratorupholstery = Configurator.GetInstance(this.HttpContext);

            if (!configuratorfill.isMaterialExists(MaterialType.UPHOLSTERY) | !configuratorupholstery.isMaterialExists(MaterialType.UPHOLSTERY))
            {
                return HttpNotFound();
            }

            int idfill = configuratorfill.GetMaterialId(MaterialType.FILL);
            int idup = configuratorupholstery.GetMaterialId(MaterialType.UPHOLSTERY);

            var queryfill = from m in db.Materials
                        where m.Id == idfill
                        select m;
            var queryup = from m in db.Materials
                        where m.Id == idup
                        select m;

            var materialfill = queryfill.FirstOrDefault();
            var materialup = queryup.FirstOrDefault();

            if (materialfill == null | materialup == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaterialfillId = idfill.ToString();
            ViewBag.MaterialupId = idup.ToString();
            return View();
        }
        public ActionResult MaterialList(int? page, int? type, int returnurl)
        {
            if (page == null | type == null)
                return RedirectToAction("List");

            var Materials = db.Materials;
            var TypeChosed = new MaterialType();

            if (type == 1)
                TypeChosed = MaterialType.FILL;
            else if (type == 2)
                TypeChosed = MaterialType.UPHOLSTERY;
            else
                TypeChosed = MaterialType.FILL;

            var query = from a in Materials
                        where a.Type == TypeChosed
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