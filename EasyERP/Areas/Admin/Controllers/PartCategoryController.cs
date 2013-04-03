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
    public class PartCategoryController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //
        // GET: /Admin/PartCategory/

        public ActionResult Index()
        {
            return View(db.PartCategories.ToList());
        }

        //
        // GET: /Admin/PartCategory/Details/5

        public ActionResult Details(int id = 0)
        {
            PartCategory partcategory = db.PartCategories.Find(id);
            if (partcategory == null)
            {
                return HttpNotFound();
            }
            return View(partcategory);
        }

        //
        // GET: /Admin/PartCategory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/PartCategory/Create

        [HttpPost]
        public ActionResult Create(PartCategory partcategory)
        {
            if (ModelState.IsValid)
            {
                db.PartCategories.Add(partcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partcategory);
        }

        //
        // GET: /Admin/PartCategory/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PartCategory partcategory = db.PartCategories.Find(id);
            if (partcategory == null)
            {
                return HttpNotFound();
            }
            return View(partcategory);
        }

        //
        // POST: /Admin/PartCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(PartCategory partcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partcategory);
        }

        //
        // GET: /Admin/PartCategory/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PartCategory partcategory = db.PartCategories.Find(id);
            if (partcategory == null)
            {
                return HttpNotFound();
            }
            return View(partcategory);
        }

        //
        // POST: /Admin/PartCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PartCategory partcategory = db.PartCategories.Find(id);
            db.PartCategories.Remove(partcategory);
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