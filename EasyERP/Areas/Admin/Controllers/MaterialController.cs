using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;
using EasyERP.Helpers;
using EasyERP.App_GlobalResources;
using EasyERP.Filters;
using System.IO;

namespace EasyERP.Areas.Admin.Controllers
{
    [CustomAuthorization(Roles = UserRole.Administrator)]
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
            ViewBag.Name = name;
            return View(materials);
        }

        [HttpGet]
        public ActionResult Index(string name = "", int type = -1)
        {
            var query1 = from q in db.MaterialTypes
                         where q.Id == type
                         select q;

            var materialType = query1.FirstOrDefault();

            if (materialType == null)
            {
                return Index(name);
            }

            var query = from s in db.Materials.Include(p => p.Type)
                        where s.Name.Contains(name) && s.TypeId == type
                        orderby s.Id descending
                        select s;

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
        public ActionResult Create(Material material, HttpPostedFileBase file = null)
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
                    db.Materials.Add(material);
                    db.SaveChanges();

                    if (iu != null && iu.IsValid())
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.AppendFormat("{0}.{1}", material.Id.ToString(), file.FileName.Split('.')[1]);
                        material.ImageName = sb.ToString();
                        iu.Save(Path.GetFileName(material.ImageName), Server.MapPath("~/Images/Materials"));
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
        public ActionResult Edit(Material material, HttpPostedFileBase file = null)
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
                    if (iu != null && iu.IsValid())
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.AppendFormat("{0}.{1}", material.Id.ToString(), file.FileName.Split('.')[1]);
                        material.ImageName = sb.ToString();
                        iu.Save(Path.GetFileName(material.ImageName), Server.MapPath("~/Images/Materials"));
                    }

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

        public ActionResult Types()
        {
            var query = from q in db.MaterialTypes
                        select q;

            var materialTypes = query.ToList();

            return View(materialTypes);
        }

        public ActionResult CreateType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateType(MaterialType materialType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.MaterialTypes.Add(materialType);
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
                    Resources.AdminControllerCreateWarning,
                    FlashMessageHelper.TypeOption.Warning);
            }

            return View(materialType);
        }

        public ActionResult EditType(int id = 0)
        {
            var query = from q in db.MaterialTypes
                        where q.Id == id
                        select q;

            var materialType = query.FirstOrDefault();

            if (materialType == null)
            {
                return HttpNotFound();
            }
            return View(materialType);
        }

        [HttpPost]
        public ActionResult EditType(MaterialType materialType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(materialType).State = EntityState.Modified;
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

            return View(materialType);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}