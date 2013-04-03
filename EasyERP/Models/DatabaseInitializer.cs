using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            /* Klienci */
            var customers = new List<Customer>
            {
                new Customer { Name = "A", City = "A", ZipCode = "A", Street = "A", Telephone = "A", Email = "A" },
                new Customer { Name = "B", City = "B", ZipCode = "B", Street = "B", Telephone = "B", Email = "B" },
                new Customer { Name = "C", City = "C", ZipCode = "C", Street = "C", Telephone = "C", Email = "C" },
            };

            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();

            /* Dostawcy */
            var suppliers = new List<Supplier>
            {
                new Supplier { NIP = "X", Name = "X", City = "X", ZipCode = "X", Street = "X", Telephone = "X", Email = "X", BankAccount = "X" },
                new Supplier { NIP = "Y", Name = "Y", City = "Y", ZipCode = "Y", Street = "Y", Telephone = "Y", Email = "Y", BankAccount = "Y" },
                new Supplier { NIP = "Z", Name = "Z", City = "Z", ZipCode = "Z", Street = "Z", Telephone = "Z", Email = "Z", BankAccount = "Z" },
            };

            suppliers.ForEach(s => context.Suppliers.Add(s));
            context.SaveChanges();

            /* Kategorie produktów */
            var productCategories = new List<ProductCategory>
            {
                new ProductCategory { Name = "Długopis" },
                new ProductCategory { Name = "Samochód" }
            };

            productCategories.ForEach(p => context.ProductCategories.Add(p));
            context.SaveChanges();

            /* Kategorie części */
            var partCategories = new List<PartCategory>
            {
                new PartCategory { ProductId = 1, Name = "Obudowa" },
                new PartCategory { ProductId = 1, Name = "Wkład" },
                new PartCategory { ProductId = 2, Name = "Silnik" },
                new PartCategory { ProductId = 2, Name = "Opony" }
            };

            partCategories.ForEach(p => context.PartCategories.Add(p));
            context.SaveChanges();

            /* Części */
            var parts = new List<Part>
            {
                new Part { SupplierId = 1, TypeId = 1, Name = "XObudowa1", Price = 1.0m, Availability = false },
                new Part { SupplierId = 1, TypeId = 1, Name = "XObudowa2", Price = 1.0m, Availability = true  },
                new Part { SupplierId = 1, TypeId = 2, Name = "XWkład1", Price = 1.0m, Availability = true  },

                new Part { SupplierId = 2, TypeId = 1, Name = "YObudowa1", Price = 1.0m, Availability = true  },
                new Part { SupplierId = 2, TypeId = 2, Name = "YWkład1", Price = 1.0m, Availability = true  },
                new Part { SupplierId = 2, TypeId = 2, Name = "YWkład2", Price = 1.0m, Availability = true  },
                new Part { SupplierId = 2, TypeId = 2, Name = "YWkład3", Price = 1.0m, Availability = true  },

                new Part { SupplierId = 3, TypeId = 3, Name = "ZSilnik1", Price = 1.0m, Availability = true  },
                new Part { SupplierId = 3, TypeId = 3, Name = "ZSilnik2", Price = 1.0m, Availability = true  },
                new Part { SupplierId = 3, TypeId = 4, Name = "ZOpony1", Price = 1.0m, Availability = true  },
                new Part { SupplierId = 3, TypeId = 4, Name = "ZOpony2", Price = 1.0m, Availability = false  },
                new Part { SupplierId = 3, TypeId = 4, Name = "ZOpony3", Price = 1.0m, Availability = true  }
            };

            parts.ForEach(p => context.Parts.Add(p));
            context.SaveChanges();

            /* Zamówienia */
            var orders = new List<Order>
            {
                new Order { CustomerId = 1, Price = 109.99m, CreatedAt = DateTime.Now },
                new Order { CustomerId = 2, Price = 209.99m, CreatedAt = DateTime.Now },
                new Order { CustomerId = 3, Price = 309.99m, CreatedAt = DateTime.Now }
            };

            orders.ForEach(o => context.Orders.Add(o));
            context.SaveChanges();

            /* Pozycje zamówień */
            var orderItems = new List<OrderItem>
            {
                new OrderItem { OrderId = 1, Quantity = 1 },
                new OrderItem { OrderId = 2, Quantity = 10 },
                new OrderItem { OrderId = 3, Quantity = 23 }
            };

            orderItems.ForEach(o => context.OrderItems.Add(o));
            context.SaveChanges();

            /* Ustawienia (nasze konfiguracje zamówień) */
            var settings = new List<Setting>
            {
                new Setting { OrderItemId = 1, PartId = 2 },
                new Setting { OrderItemId = 1, PartId = 3 },

                new Setting { OrderItemId = 2, PartId = 4 },
                new Setting { OrderItemId = 2, PartId = 6},

                new Setting { OrderItemId = 3, PartId = 9},
                new Setting { OrderItemId = 3, PartId = 12},
            };

            settings.ForEach(s => context.Settings.Add(s));
            context.SaveChanges();
        }
    }
}
