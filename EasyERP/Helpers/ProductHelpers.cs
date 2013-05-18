using EasyERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyERP.Helpers
{
    public class ProductHelpers
    {
        public static string DisplayTypeName(ProductType type)
        {
            switch (type)
            {
                case ProductType.ARMCHAIR:
                    return "Fotel";
                case ProductType.SOFA:
                    return "Sofa";
                case ProductType.BED:
                    return "Łóżko";
                default:
                    return null;
            }
        }

        public static SelectList GetSelectList()
        {
            var items = new[]
            {
                new SelectListItem { Value = ProductType.ARMCHAIR.ToString(), Text = DisplayTypeName(ProductType.ARMCHAIR) },
                new SelectListItem { Value = ProductType.SOFA.ToString(), Text = DisplayTypeName(ProductType.SOFA) },
                new SelectListItem { Value = ProductType.BED.ToString(), Text = DisplayTypeName(ProductType.BED) }
            };

            return new SelectList(items, "Value", "Text");
        }
        public static string DisplayCategoryName(string name)
        {
            switch (name)
            {
                case "Fotel":
                    return "Fotele";
                case "Sofa":
                    return "Sofy";
                case "Łóżko":
                    return "Łóżka";
                default:
                    return null;
            }
        }

        public static List<string> GetCategories()
        {
        List<string> Categories = new List<string>();
            Categories.Add(DisplayTypeName(ProductType.ARMCHAIR));
            Categories.Add(DisplayTypeName(ProductType.SOFA));
            Categories.Add(DisplayTypeName(ProductType.BED));
        return Categories;
        }
    }
}
