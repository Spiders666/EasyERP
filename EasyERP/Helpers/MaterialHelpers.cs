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
        public static string DisplayTypeName(MaterialType type)
        {
            switch (type)
            {
                case MaterialType.UPHOLSTERY:
                    return "Obicie";
                case MaterialType.FILL:
                    return "Wypełnienie";
                default:
                    return null;
            }
        }

        public static SelectList GetSelectList()
        {
            var items = new []
            {
                new SelectListItem { Value = MaterialType.UPHOLSTERY.ToString(), Text = DisplayTypeName(MaterialType.UPHOLSTERY) },
                new SelectListItem { Value = MaterialType.FILL.ToString(), Text = DisplayTypeName(MaterialType.FILL) }
            };

            return new SelectList(items, "Value", "Text");
        }
    }
}
