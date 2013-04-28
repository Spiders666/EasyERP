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

        [Required]
        public int UserId { get; set; }

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

        [Timestamp]
        public byte[] CurrentVersion { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
