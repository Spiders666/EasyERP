using EasyERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Helpers
{
    public class ProductHelpers
    {
        public static string DisplayTypeName(ProductType type)
        {
            switch (type)
            {
                case ProductType.CHAIR:
                    return "Fotel";
                case ProductType.SOFA:
                    return "Sofa";
                default:
                    return null;
            }
        }
    }
}
