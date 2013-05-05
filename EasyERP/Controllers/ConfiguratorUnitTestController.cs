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
            Configurator configurator = Configurator.GetInstance(this.HttpContext);

            var query = from m in db.Materials
                        where m.Id == id
                        select m;

            var material = query.FirstOrDefault();

            if (material == null)
            {
                return HttpNotFound();
            }

            configurator.SetMaterial(material.Type, material.Id);

            ViewBag.MaterialId = material.Id;
            return View();
        }

        public ActionResult Get()
        {
            Configurator configurator = Configurator.GetInstance(this.HttpContext);

            if (!configurator.isMaterialExists(MaterialType.UPHOLSTERY))
            {
                return HttpNotFound();
            }

            int id = configurator.GetMaterialId(MaterialType.UPHOLSTERY);

            var query = from m in db.Materials
                        where m.Id == id
                        select m;

            var material = query.FirstOrDefault();

            if (material == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaterialId = id.ToString();
            return View();
        }
    }
}
