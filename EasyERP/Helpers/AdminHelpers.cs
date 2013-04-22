using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;

namespace EasyERP.Helpers
{
    public static class AdminHelpers
    {
        public static string DisplayOrderStateName(this HtmlHelper helper, int state)
        {
            switch (state)
            {
                case Order.States.NOT_CONFIRMED:
                    return "Nie potwierdzone";
                case Order.States.CANCELED:
                    return "Anulowane";
                case Order.States.EXPECTED:
                    return "Oczekiwane";
                case Order.States.SENT:
                    return "Wysłane";
            }

            return null;
        }

        public static string DisplayMaterialTypeName(this HtmlHelper helper, int type)
        {
            switch (type)
            {
                case Material.Types.UPHOLSTERY:
                    return "Obicie";
                case Material.Types.FILL:
                    return "Wypełnienie";
            }

            return null;
        }

        public static string DisplayProductTypeName(this HtmlHelper helper, int type)
        {
            switch (type)
            {
                case Product.Types.CHAIR:
                    return "Fotel";
                case Product.Types.SOFA:
                    return "Sofa";
            }

            return null;
        }

        public static string DisplayPolishFormatPrice(this HtmlHelper helper, decimal price)
        {
            return String.Format("{0:0.00} zł", price);
        }
    }
}
