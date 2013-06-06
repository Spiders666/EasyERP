using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;
using System.Web.Mvc;

namespace EasyERP.Filters
{
    public class CustomAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DatabaseContext", "UserProfile", "UserId", "UserName", autoCreateTables: false);
            }
            return base.AuthorizeCore(httpContext);
        }
    }
}
