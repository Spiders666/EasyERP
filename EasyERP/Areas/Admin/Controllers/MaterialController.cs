using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;
using EasyERP.Helpers;

namespace EasyERP.Areas.Admin.Controllers
{
    [Authorize(Roles = UserRole.Administrator)]
    public class MaterialController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index(string name = "")
        {
            var query = from q in db.Materials.Include(m => m.Type)
                        where q.Name.Contains(name)
                        orderby q.Id descending
                        select q;

            var materials = query.ToList();

            return View(materials);
        }

        public ActionResult Details(int id = 0)
        {
            var query = from m in db.Materials.Include(m => m.Supplier).Include(m => m.Type)
                        where m.Id == id
                        select m;

            var material = query.FirstOrDefault();

            if (material == null || material.Supplier == null)
            {
                return HttpNotFound();
            }

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
            try
            {
                if (ModelState.IsValid)
                {
                    db.Materials.Add(material);
                    db.SaveChanges();
                    FlashMessageHelper.SetMessage(
                        this,
                        HttpContext.GetGlobalResourceObject(
                            "Resources",
                            "AdminControllerCreateSuccess").ToString(),
                        FlashMessageHelper.TypeOption.Success
                    );
                    return RedirectToAction("Index");
                }

                FlashMessageHelper.SetMessage(
                    this,
                    HttpContext.GetGlobalResourceObject(
                        "Resources",
                        "AdminControllerCreateError").ToString(),
                    FlashMessageHelper.TypeOption.Error
                );
            }
            catch (Exception)
            {
                FlashMessageHelper.SetMessage(
                    this,
                    HttpContext.GetGlobalResourceObject(
                        "Resources",
                        "AdminControllerCreateWarning").ToString(),
                    FlashMessageHelper.TypeOption.Warning
                );
            }

            return View(material);
        }

        public ActionResult Edit(int id = 0)
        {
            var query = from s in db.Materials
                        where s.Id == id
                        select s;

            var material = query.FirstOrDefault();

            if (material == null)
            {
                return HttpNotFound();
            }

            return View(material);
        }

        [HttpPost]
        public ActionResult Edit(Material material)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(material).State = EntityState.Modified;
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