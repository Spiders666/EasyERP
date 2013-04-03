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
    public class PartController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //
        // GET: /Admin/Part/

        public ActionResult Index()
        {
            var parts = db.Parts.Include(p => p.Supplier).Include(p => p.Type);
            return View(parts.ToList());
        }

        //
        // GET: /Admin/Part/Details/5

        public ActionResult Details(int id = 0)
        {
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        //
        // GET: /Admin/Part/Create

        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "NIP");
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name");
            return View();
        }

        //
        // POST: /Admin/Part/Create

        [HttpPost]
        public ActionResult Create(Part part)
        {
            if (ModelState.IsValid)
            {
                db.Parts.Add(part);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "NIP", part.SupplierId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", part.TypeId);
            return View(part);
        }

        //
        // GET: /Admin/Part/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "NIP", part.SupplierId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", part.TypeId);
            return View(part);
        }

        //
        // POST: /Admin/Part/Edit/5

        [HttpPost]
        public ActionResult Edit(Part part)
        {
            if (ModelState.IsValid)
            {
                db.Entry(part).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "NIP", part.SupplierId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", part.TypeId);
            return View(part);
        }

        //
        // GET: /Admin/Part/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        //
        // POST: /Admin/Part/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Part part = db.Parts.Find(id);
            db.Parts.Remove(part);
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