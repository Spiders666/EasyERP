using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;
using EasyERP.Helpers;
using System.Data.Entity.Infrastructure;

namespace EasyERP.Areas.Admin.Controllers
{
    [Authorize(Roles = UserRole.Administrator)]
    public class SupplierController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index(string name = "")
        {
            var query = from q in db.Suppliers
                        where q.Name.Contains(name)
                        orderby q.Id descending
                        select q;

            var suppliers = query.ToList();

            return View(suppliers);
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

            return View(supplier);
        }

        public ActionResult Edit(int id = 0)
        {
            var query = from s in db.Suppliers
                        where s.Id == id
                        select s;

            var supplier = query.FirstOrDefault();

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
            catch(Exception)
            {
                FlashMessageHelper.SetMessage(
                    this,
                    HttpContext.GetGlobalResourceObject(
                        "Resources",
                        "AdminControllerEditWarning").ToString(),
                    FlashMessageHelper.TypeOption.Warning
                );
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
