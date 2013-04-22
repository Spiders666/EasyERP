using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public int ProductType { get; set; }

        public String ProductName { get; set; }

        [Column(TypeName = "money")]
        public decimal ProductPrice { get; set; }

        public int State { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        public struct States
        {
            public const int NOT_CONFIRMED = 0; //oczekiwanie na potwierdzenie zamówienia
            public const int CANCELED = 1; //zamówienie anulowane
            public const int EXPECTED = 2; //oczekiwanie zapłaty
            public const int SENT = 3; //produkt wysłany do klienta
        }
    }
}
