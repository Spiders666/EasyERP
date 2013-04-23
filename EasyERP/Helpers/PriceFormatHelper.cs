using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Helpers
{
    public class PriceFormatHelper
    {
        public static string DisplayFormatPrice(decimal price)
        {
            return String.Format("{0:0.00} zł", price);
        }
    }
}
