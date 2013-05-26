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
        public static SelectList GetSelectList()
        {
            DatabaseContext db = new DatabaseContext();
            var query = from q in db.ProductTypes
                        select q;

            var productTypes = query.ToList();

            if (productTypes == null)
            {
                return null;
            }

            return new SelectList(productTypes, "Id", "Name");
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
                    return "nieznane";
            }
        }

        public static List<string> GetCategories()
        {
            DatabaseContext db = new DatabaseContext();
            var query = from q in db.ProductTypes
                        select q;

            var productTypes = query.ToList();

            if (productTypes == null)
            {
                return  new List<string>();
            }

            List<string> result = new List<string>();

            foreach(var productType in productTypes)
            {
                result.Add(productType.Name);
            }

            return result;
        }
    }
}
