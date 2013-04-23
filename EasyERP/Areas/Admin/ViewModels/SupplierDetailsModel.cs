using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyERP.Models;
using EasyERP.Helpers;

namespace EasyERP.Areas.Admin.ViewModels
{
    public class SupplierDetailsModel
    {
        public Supplier Supplier { get; set; }
        public List<Material> Materials { get; set; }

        public SupplierDetailsModel(Supplier supplier, List<Material> materials)
        {
            this.Supplier = supplier;
            this.Materials = materials;
        }
    }
}
