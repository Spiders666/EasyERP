using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;
using EasyERP.Areas.Admin.ViewModels;
using EasyERP.Helpers;

namespace EasyERP.Areas.Admin.Controllers
{
    public class MaterialController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            var query = from m in db.Materials select m;
            var materials = query.ToList();

            return View(materials);
        }

        public ActionResult Details(int id = 0)
        {
            var query = from m in db.Materials.Include(m => m.Supplier)
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
                    FlashMessageHelper.SetMessage(this, "Zapisanie nowych danych przebiegło pomyślnie.", FlashMessageHelper.TypeOption.Success);
                    return RedirectToAction("Index");
                }

                FlashMessageHelper.SetMessage(this, "Wystąpił błąd podczas zapisywania nowych danych. Należy poprawić zaistniałe błędy.", FlashMessageHelper.TypeOption.Error);
            }
            catch (Exception)
            {
                FlashMessageHelper.SetMessage(this, "Z niewiadomych przyczyn nowe dane nie zostały zapisane.", FlashMessageHelper.TypeOption.Warning);
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
                    FlashMessageHelper.SetMessage(this, "Aktualizacja danych przebiegła pomyślnie.", FlashMessageHelper.TypeOption.Success);
                    return RedirectToAction("Index");
                }

                FlashMessageHelper.SetMessage(this, "Wystąpił błąd podczas aktualizacji. Należy poprawić dane.", FlashMessageHelper.TypeOption.Error);
            }
            catch (Exception)
            {
                FlashMessageHelper.SetMessage(this, "Dane został zaktualizowane przez inną osobę. Należy odświeżyć stronę w celu wczytania nowych danych.", FlashMessageHelper.TypeOption.Warning);
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