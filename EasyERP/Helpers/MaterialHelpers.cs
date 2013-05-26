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
        public static SelectList GetSelectList()
        {
            DatabaseContext db = new DatabaseContext();
            var query = from q in db.MaterialTypes
                        select q;

            var materialTypes = query.ToList();

            if (materialTypes == null)
            {
                return null;
            }

            return new SelectList(materialTypes, "Id", "Name");
        }
    }
}
