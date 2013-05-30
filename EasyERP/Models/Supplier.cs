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
        [Display(Name = "NIP")]
        public string NIP { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Kod pocztowy")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Telefon")]
        public string Telephone { get; set; }

        public string Email { get; set; }

        [Required]
        [Display(Name = "Numer konta bankowego")]
        public string BankAccount { get; set; }

        [Timestamp]
        public byte[] CurrentVersion { get; set; }

        public ICollection<Material> Materials { get; set; }
    }
}
