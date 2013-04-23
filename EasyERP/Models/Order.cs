using System;
using System.Collections.Generic;
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
        public int CustomerId { get; set; }

        public ProductType ProductType { get; set; }

        public String ProductName { get; set; }

        [Column(TypeName = "money")]
        public decimal ProductPrice { get; set; }

        public OrderState State { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
