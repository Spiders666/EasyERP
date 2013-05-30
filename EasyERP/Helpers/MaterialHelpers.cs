using EasyERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            result.Insert(0, new SelectListItem { Value = "", Text = "-- rodzaj materiału --" });

            return new SelectList(result, "Value", "Text");
        }
    }
}
