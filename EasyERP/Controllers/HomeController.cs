﻿using EasyERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace EasyERP.Controllers
{
    public class HomeController : Controller
    {


        private DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DatabaseContext", "UserProfile", "UserId",
                                                         "UserName", autoCreateTables: true);
            }
            var products = (from p in db.Products
                            where p.Availability == true
                            orderby p.Id descending
                            select p).Take(9);
            return View(products);
            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Strona opisu.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Strona Kontaktowa.";

            return View();
        }
    }
}
