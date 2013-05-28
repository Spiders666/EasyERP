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
        [Display(Name = "Imię")]
        [StringLength(100, ErrorMessage = "{0} musi składać się z conajmniej {2} znaków.", MinimumLength = 6)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        [StringLength(100, ErrorMessage = "{0} musi składać się z conajmniej {2} znaków.", MinimumLength = 6)]
        public string SurName { get; set; }

        [Required]
        [Display(Name = "Miasto")]
        [StringLength(30, ErrorMessage = "{0} musi składać się z conajmniej {2} znaków.", MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [Display(Name = "Kod pocztowy")]
        [RegularExpression(@"^\d{2}(-\d{3})?$", ErrorMessage="Niepoprawny format kodu pocztowego (xx-xxx)")]
        [StringLength(6, ErrorMessage = "{0} musi składać się z {2} znaków.", MinimumLength = 6)]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Nazwa ulicy")]
        [StringLength(30, ErrorMessage = "{0} musi składać się z conajmniej {2} znaków.", MinimumLength = 3)]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Numer Telefonu")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Niepoprawny format numeru telefonu! (xxx-xxx-xxx)")]
        [StringLength(9, ErrorMessage = "{0} musi składać się z {2} cyfr.", MinimumLength = 9)]
        public string Telephone { get; set; }

        [Required]
        [Display(Name = "Adres email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+]+)*\\.([a-z]{2,4})$", ErrorMessage= "Niepoprawny format adresu! (x@x.x)")]
        public string Email { get; set; }

        [Display(Name = "Link aktywacyjny")]
        public string ActivationLink { get; set; }

        [Required]
        [Display(Name = "Aktywacja")]
        public bool Activation { get; set; }

        [Timestamp]
        public byte[] CurrentVersion { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}