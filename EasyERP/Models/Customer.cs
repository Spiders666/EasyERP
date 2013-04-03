using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
