using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public enum OrderState
    {
        NOT_CONFIRMED = 0, //oczekiwanie na potwierdzenie zamówienia
        CANCELED = 1, //zamówienie anulowane
        PENDING = 2, //oczekiwanie zapłaty
        SENT = 3 //produkt wysłany do klienta
    }

    public class Order
    {
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public String ProductName { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal ProductPrice { get; set; }

        [Required]
        public OrderState State { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
