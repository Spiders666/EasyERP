using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        public string NIP { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Telephone { get; set; }

        public string Email { get; set; }

        [Required]
        public string BankAccount { get; set; }

        [Timestamp]
        public byte[] CurrentVersion { get; set; }

        public ICollection<Material> Materials { get; set; }
    }
}
