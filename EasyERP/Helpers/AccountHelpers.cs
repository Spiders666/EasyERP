using EasyERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using WebMatrix.WebData;
using System.Web.Mvc.Html;

namespace EasyERP.Helpers
{
    public class AccountHelpers
    {
        public static bool CheckAdminRole()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DatabaseContext", "UserProfile", "UserId", "UserName", autoCreateTables: false);
            }

            return Roles.IsUserInRole(UserRole.Administrator) ? true : false;
        }
    }
}
