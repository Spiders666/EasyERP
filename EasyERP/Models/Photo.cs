using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Source { get; set; }
    }
}
