using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;


namespace EasyERP.Controllers
{
    public class ConfiguratorUnitTestController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Set(int id = 0)
        {
            SessionSettings sessionSettings = SessionSettings.GetInstance(this.HttpContext);

            var query = from m in db.Materials.Include("Type")
                        where m.Id == id
                        select m;

            var material = query.FirstOrDefault();

            if (material == null)
            {
                return HttpNotFound();
            }

            sessionSettings.SetMaterial(material.Type.Id, material.Id);
            sessionSettings.SetMaterial(2, 8);

            ViewBag.MaterialId = material.Id;
            return View();
        }
        
        public ActionResult Get()
        {
            SessionSettings sessionSettings = SessionSettings.GetInstance(this.HttpContext);

            if (!sessionSettings.isMaterialExists(2))
            {
                return HttpNotFound();
            }

            int materialfillId = sessionSettings.GetMaterialId(2);

            var query = from m in db.Materials
                        where m.Id == materialfillId
                        select m;

            var material = query.FirstOrDefault();

            if (material == null)
            {
                return HttpNotFound();
            }

            if (!sessionSettings.isMaterialExists(1))
            {
                return HttpNotFound();
            }

            int materialupId = sessionSettings.GetMaterialId(1);

            var query2 = from m in db.Materials
                         where m.Id == materialupId
                         select m;

            var material2 = query.FirstOrDefault();

            if (material2 == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaterialfillId = materialfillId.ToString();
            ViewBag.MaterialupId = materialupId.ToString();
            return View();
        }
        
    }
}
