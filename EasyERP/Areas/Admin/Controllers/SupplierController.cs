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
using System.Data.Entity.Infrastructure;

namespace EasyERP.Areas.Admin.Controllers
{
    public class SupplierController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View(db.Suppliers.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            var query = from s in db.Suppliers.Include(s => s.Materials)
                        where s.Id == id
                        select s;

            var supplier = query.FirstOrDefault();

            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Suppliers.Add(supplier);
                    db.SaveChanges();
                    FlashMessageHelper.SetMessage(this, "Nowy dostawca został dodany do bazy.", FlashMessageHelper.TypeOption.Success);
                    return RedirectToAction("Index");
                }

                FlashMessageHelper.SetMessage(this, "Wystąpił błąd podczas zapisywania nowych danych. Należy poprawić zaistniałe błędy.", FlashMessageHelper.TypeOption.Error);
            }
            catch (Exception)
            {
                FlashMessageHelper.SetMessage(this, "Z niewiadomych przyczyn dane nie zostały utworzone.", FlashMessageHelper.TypeOption.Warning);
            }

            return View(supplier);
        }

        public ActionResult Edit(int id = 0)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        [HttpPost]
        public ActionResult Edit(Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(supplier).State = EntityState.Modified;
                    db.SaveChanges();
                    FlashMessageHelper.SetMessage(this, "Dane zostały zaktualizowane.", FlashMessageHelper.TypeOption.Success);
                    return RedirectToAction("Index");
                }

                FlashMessageHelper.SetMessage(this, "Wystąpił błąd podczas aktualizacji. Należy poprawić dane.", FlashMessageHelper.TypeOption.Error);
            }
            catch(Exception)
            {
                FlashMessageHelper.SetMessage(this, "Dane został zaktualizowane przez inną osobę. Należy odświeżyć daną stronę w celu wczytania nowych danych.", FlashMessageHelper.TypeOption.Warning);
            }

            return View(supplier);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
