﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;
using EasyERP.Helpers;
using EasyERP.App_GlobalResources;

namespace EasyERP.Areas.Admin.Controllers
{
    [Authorize(Roles = UserRole.Administrator)]
    public class ProductController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index(string name = "")
        {
            var query = from s in db.Products.Include(p => p.Type)
                        where s.Name.Contains(name)
                        orderby s.Id descending
                        select s;

            var products = query.ToList();

            return View(products);
        }

        public ActionResult Details(int id = 0)
        {
            var query = from s in db.Products.Include(p => p.Type)
                        where s.Id == id
                        select s;

            var product = query.FirstOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
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

            return View(product);
        }

        public ActionResult Edit(int id = 0)
        {
            var query = from s in db.Products
                        where s.Id == id
                        select s;

            var product = query.FirstOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
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

            return View(product);
        }


        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /**
         * Product types
         */

        public ActionResult Types()
        {
            var query = from q in db.ProductTypes
                        select q;

            var productTypes = query.ToList();

            return View(productTypes);
        }

        public ActionResult CreateType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateType(ProductType productType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ProductTypes.Add(productType);
                    db.SaveChanges();
                    FlashMessageHelper.SetMessage(this,
                        Resources.AdminControllerCreateSuccess,
                        FlashMessageHelper.TypeOption.Success);

                    return RedirectToAction("Types");
                }

                FlashMessageHelper.SetMessage(this,
                    Resources.AdminControllerCreateError,
                    FlashMessageHelper.TypeOption.Error);
            }
            catch (Exception)
            {
                FlashMessageHelper.SetMessage(this,
                    Resources.AdminControllerEditWarning,
                    FlashMessageHelper.TypeOption.Warning);
            }

            return View(productType);
        }

        public ActionResult EditType(int id = 0)
        {
            var query = from q in db.ProductTypes
                        where q.Id == id
                        select q;

            var productType = query.FirstOrDefault();

            if (productType == null)
            {
                return HttpNotFound();
            }
            return View(productType);
        }

        [HttpPost]
        public ActionResult EditType(ProductType productType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ProductTypes.Add(productType);
                    db.SaveChanges();
                    FlashMessageHelper.SetMessage(this,
                        Resources.AdminControllerCreateSuccess,
                        FlashMessageHelper.TypeOption.Success);

                    return RedirectToAction("Types");
                }

                FlashMessageHelper.SetMessage(this,
                    Resources.AdminControllerCreateError,
                    FlashMessageHelper.TypeOption.Error);
            }
            catch (Exception)
            {
                FlashMessageHelper.SetMessage(this,
                    Resources.AdminControllerEditWarning,
                    FlashMessageHelper.TypeOption.Warning);
            }

            return View(productType);
        }

        public ActionResult SetTypeConfiguration(int id = 0)
        {
            var query = from q in db.MaterialTypes
                        select q;

            var materialTypes = query.ToList();

            if (materialTypes == null)
            {
                return HttpNotFound();
            }

            return View(materialTypes);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}