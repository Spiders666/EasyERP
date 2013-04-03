using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        public string NIP { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string BankAccount { get; set; }

        public ICollection<Part> Parts { get; set; }
    }
}
