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
using EasyERP.Areas.Admin.ViewModels;
using EasyERP.Filters;
using System.IO;

namespace EasyERP.Areas.Admin.Controllers
{
    [CustomAuthorization(Roles = UserRole.Administrator)]
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
            ViewBag.Name = name;
            return View(products);
        }

        [HttpGet]
        public ActionResult Index(string name = "", int type = -1)
        {
            var query1 = from q in db.ProductTypes
                         where q.Id == type
                         select q;

            var productType = query1.FirstOrDefault();

            if (productType == null)
            {
                return Index(name);
            }

            var query = from s in db.Products.Include(p => p.Type)
                        where s.Name.Contains(name) && s.TypeId == type
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
            return View(new Product());
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file = null)
        {
            try
            {
                ImageUploader iu = null;

                if (file != null)
                {
                    iu = new ImageUploader(this, file, "ImageName");
                    iu.Validate();
                }

                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    db.SaveChanges();

                    if (iu != null && iu.IsValid())
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.AppendFormat("{0}.{1}", product.Id.ToString(), file.FileName.Split('.')[1]);
                        product.ImageName = sb.ToString();
                        iu.Save(Path.GetFileName(product.ImageName), Server.MapPath("~/Images/Products"));
                        db.SaveChanges();
                    }

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
        public ActionResult Edit(Product product, HttpPostedFileBase file = null)
        {
            ImageUploader iu = null;

            if (file != null)
            {
                iu = new ImageUploader(this, file, "ImageName");
                iu.Validate();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    if (iu != null && iu.IsValid())
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.AppendFormat("{0}.{1}", product.Id.ToString(), file.FileName.Split('.')[1]);
                        product.ImageName = sb.ToString();
                        iu.Save(Path.GetFileName(product.ImageName), Server.MapPath("~/Images/Products"));
                    }

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();

                    FlashMessageHelper.SetMessage(this,
                        Resources.AdminControllerEditSuccess,
                        FlashMessageHelper.TypeOption.Success);

                    return RedirectToAction("Index");
                }

                FlashMessageHelper.SetMessage(this,
                    Resources.AdminControllerEditError,
                    FlashMessageHelper.TypeOption.Error);
            }
            catch (Exception)
            {
                FlashMessageHelper.SetMessage(this,
                    Resources.AdminControllerEditWarning,
                    FlashMessageHelper.TypeOption.Warning);
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
                    db.Entry(productType).State = EntityState.Modified;
                    db.SaveChanges();
                    FlashMessageHelper.SetMessage(this,
                        Resources.AdminControllerEditSuccess,
                        FlashMessageHelper.TypeOption.Success);

                    return RedirectToAction("Types");
                }

                FlashMessageHelper.SetMessage(this,
                    Resources.AdminControllerEditError,
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
            var query = from q in db.ProductTypes
                        where q.Id == id
                        select q;

            var productType = query.FirstOrDefault();

            if (productType == null)
            {
                return HttpNotFound();
            }

            var query2 = from q in db.MaterialTypes
                         select q;

            var materialTypes = query2.ToList();

            var query3 = from q in db.Configurations
                         where q.ProductTypeId == productType.Id
                         select q.MaterialType;

            var configuration = query3.ToList();

            ConfigurationViewModel configurationViewModel = new ConfigurationViewModel();
            configurationViewModel.ProductType = productType;
            configurationViewModel.MaterialTypes = materialTypes;
            configurationViewModel.Configuration = new List<bool>();

            foreach (var materialType in materialTypes)
            {
                configurationViewModel.Configuration.Add(configuration.Contains(materialType));
            }

            return View(configurationViewModel);
        }

        [HttpPost]
        public ActionResult SetTypeConfiguration(ConfigurationViewModel configurationViewModelCommand)
        {
            var query = from q in db.ProductTypes
                        where q.Id == configurationViewModelCommand.ProductType.Id
                        select q;

            var productType = query.FirstOrDefault();

            if (productType == null)
            {
                return HttpNotFound();
            }

            var query2 = from q in db.MaterialTypes
                         select q;

            var materialTypes = query2.ToList();

            if (configurationViewModelCommand.Configuration.Count != materialTypes.Count)
            {
                return HttpNotFound();
            }

            var query3 = from q in db.Configurations.Include(c => c.MaterialType)
                         where q.ProductTypeId == productType.Id
                         select q;

            var configuration = query3.ToList();

            ConfigurationViewModel configurationViewModel = new ConfigurationViewModel();
            configurationViewModel.ProductType = productType;
            configurationViewModel.MaterialTypes = materialTypes;
            configurationViewModel.Configuration = configurationViewModelCommand.Configuration;

            try
            {
                int i = 0;
                bool test1, test2;

                foreach (var materialType in materialTypes)
                {
                    test1 = configuration.Find(c => c.MaterialTypeId == materialType.Id) != null;
                    test2 = configurationViewModelCommand.Configuration[i];

                    if (test1 == true && test2 == false)
                    {
                        db.Configurations.Remove(configuration.Find(m => m.MaterialTypeId == materialType.Id));
                    }

                    else if (test1 == false && test2 == true)
                    {
                        db.Configurations.Add(new Configuration { MaterialTypeId = materialType.Id, ProductTypeId = productType.Id });
                    }

                    i++;
                }

                db.SaveChanges();

                FlashMessageHelper.SetMessage(this,
                    Resources.AdminControllerEditSuccess,
                    FlashMessageHelper.TypeOption.Success);

                return RedirectToAction("Types");

            }
            catch(Exception)
            {
                FlashMessageHelper.SetMessage(this,
                    Resources.AdminControllerEditError,
                    FlashMessageHelper.TypeOption.Error);
            }

            return View(configurationViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}