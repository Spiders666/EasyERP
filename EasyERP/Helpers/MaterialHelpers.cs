using EasyERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}
