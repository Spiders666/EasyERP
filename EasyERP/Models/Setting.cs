using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PartId { get; set; }
    }
}
