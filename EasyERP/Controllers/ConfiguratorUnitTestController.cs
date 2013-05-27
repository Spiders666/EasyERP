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

namespace EasyERP.Controllers
{
    public class ConfiguratorUnitTestController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Set(int id = 0)
        {
            SessionSettings sessionSettings = SessionSettings.GetInstance(this.HttpContext);

            var query = from q in db.Materials.Include(m => m.Type)
                        where q.Id == id
                        select q;

            var material = query.FirstOrDefault();

            if (material == null)
            {
                return HttpNotFound();
            }

            sessionSettings.SetMaterial(material.Type.Id, material.Id);

            ViewBag.MaterialId = material.Id;
            return View();
        }
        
        public ActionResult Get()
        {
            SessionSettings sessionSettings = SessionSettings.GetInstance(this.HttpContext);

            var query = from q in db.Configurations
                        select q.MaterialType;

            var materialTypes = query.ToList();

            List<int> listId= new List<int>();

            foreach (var materialType in materialTypes)
            {
                listId.Add(sessionSettings.GetMaterialId(materialType.Id));
            }

            return View(listId);
        }
        
    }
}
