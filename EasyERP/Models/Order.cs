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
        NotConfirmed = 0, //oczekiwanie na potwierdzenie zamówienia
        Canceled = 1, //zamówienie anulowane
        Pending = 2, //oczekiwanie zapłaty
        Sent = 3 //produkt wysłany do klienta
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
