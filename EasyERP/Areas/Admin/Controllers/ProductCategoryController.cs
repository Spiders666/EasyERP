using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;

namespace EasyERP.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //
        // GET: /Admin/ProductCategory/

        public ActionResult Index()
        {
            return View(db.ProductCategories.ToList());
        }

        //
        // GET: /Admin/ProductCategory/Details/5

        public ActionResult Details(int id = 0)
        {
            ProductCategory productcategory = db.ProductCategories.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        //
        // GET: /Admin/ProductCategory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/ProductCategory/Create

        [HttpPost]
        public ActionResult Create(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                db.ProductCategories.Add(productcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productcategory);
        }

        //
        // GET: /Admin/ProductCategory/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProductCategory productcategory = db.ProductCategories.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        //
        // POST: /Admin/ProductCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productcategory);
        }

        //
        // GET: /Admin/ProductCategory/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProductCategory productcategory = db.ProductCategories.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        //
        // POST: /Admin/ProductCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCategory productcategory = db.ProductCategories.Find(id);
            db.ProductCategories.Remove(productcategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}