using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using EasyERP.Models;

namespace EasyERP.Areas.Admin.ViewModels
{
    public class ConfigurationViewModel
    {
        public ProductType ProductType { get; set; }
        public List<MaterialType> MaterialTypes { get; set; }
        public List<bool> Configuration { get; set; }
    }
}
