using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;
using EasyERP.Areas.Admin.ViewModels;

namespace EasyERP.Areas.Admin.Controllers
{
    public class MaterialController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View(db.Materials.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            var query = from m in db.Materials.Include(m => m.Supplier)
                        where m.Id == id
                        select m;

            var material = query.FirstOrDefault();

            if (material == null)
            {
                return HttpNotFound();
            }

            Supplier supplier = db.Suppliers.Find(material.SupplierId);

            return View(material);
        }
        
        public ActionResult Create(int supplierId = 0)
        {
            var query = from s in db.Suppliers
                        where s.Id == supplierId
                        select s;

            var supplier = query.FirstOrDefault();

            if (supplier == null)
            {
                return HttpNotFound();
            }

            var material = new Material();
            material.SupplierId = supplier.Id;

            return View(material);
        }

        [HttpPost]
        public ActionResult Create(Material material)
        {
            if (ModelState.IsValid)
            {
                db.Materials.Add(material);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(material);
        }

        public ActionResult Edit(int id = 0)
        {
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        [HttpPost]
        public ActionResult Edit(Material material)
        {
            if (ModelState.IsValid)
            {
                db.Entry(material).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(material);
        }

        public ActionResult Delete(int id = 0)
        {
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Material material = db.Materials.Find(id);
            db.Materials.Remove(material);
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