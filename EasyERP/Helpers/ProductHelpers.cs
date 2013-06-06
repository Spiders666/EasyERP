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

            List<SelectListItem> result = new SelectList(productTypes, "Id", "Name").ToList();

            result.Insert(0, new SelectListItem { Value = "", Text = "-- rodzaj produktu --" });

            return new SelectList(result, "Value", "Text");
        }

        public static int GetIdOnCatogory(string category)
        {
            DatabaseContext db = new DatabaseContext();
            var Query = from q in db.ProductTypes
                        where q.Name == category
                        select q.Id;
            var GetQuery = Query.FirstOrDefault();
            return GetQuery;
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
