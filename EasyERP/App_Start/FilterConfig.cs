using EasyERP.Filters;
using System.Web;
using System.Web.Mvc;

namespace EasyERP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}