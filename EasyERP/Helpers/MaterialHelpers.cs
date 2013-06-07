using EasyERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Data;
using WebMatrix.WebData;

namespace EasyERP.Helpers
{
    public class MaterialHelpers
    {
        public static List<MaterialType> GetList()
        {
            DatabaseContext db = new DatabaseContext();

            var query = from q in db.MaterialTypes
                        select q;

            var materialTypes = query.ToList();

            return materialTypes;
        }

        public static SelectList GetSelectList()
        {
            List<SelectListItem> result = new SelectList(GetList(), "Id", "Name").ToList();

            result.Insert(0, new SelectListItem { Value = "", Text = "-- typ materiału --" });

            return new SelectList(result, "Value", "Text");
        }

        public static Material GetMaterial (int typeid, List<Material> materials ) {
            var material = materials.Find(m => m.TypeId == typeid);
            if (material == null)
            {
                return new Material();
            }
            return material;
        }

        public static string GetCategoryName(int id)
        {
            DatabaseContext db = new DatabaseContext();
            var query = from q in db.Products.Include(p => p.Type)
                        where q.Id == id
                        select q.Type.Name;
            var GetQuery = query.FirstOrDefault();
            return GetQuery;
        }

        public static string GetPrice (int id) {
            DatabaseContext db = new DatabaseContext();
            var query = from q in db.Materials
                        where q.Id == id
                        select q.Price;
            if (!query.Any())
            {
                return String.Format("{0:0.00} zł", 0.00m);
            }
            var GetQuery = query.FirstOrDefault();
            return String.Format("{0:0.00} zł", GetQuery);
        }
    }
}
