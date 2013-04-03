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

            /* Produkty */
            var products = new List<Product>
            {
                new Product { Name = "Długopis" },
                new Product { Name = "Samochód" }
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();

            /* Rodzaje części produktu */
            var types = new List<Type>
            {
                new Type { ProductId = 1, Name = "Obudowa" },
                new Type { ProductId = 1, Name = "Wkład" },
                new Type { ProductId = 2, Name = "Silnik" },
                new Type { ProductId = 2, Name = "Opony" }
            };

            types.ForEach(t => context.Types.Add(t));
            context.SaveChanges();

            /* Części (nasz surowiec) */
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

            /* Ustawienia (nasze konfiguracje zamówień) */
            var settings = new List<Setting>
            {
                new Setting { OrderId = 1, PartId = 2 },
                new Setting { OrderId = 1, PartId = 3 },

                new Setting { OrderId = 2, PartId = 4 },
                new Setting { OrderId = 2, PartId = 6},

                new Setting { OrderId = 3, PartId = 9},
                new Setting { OrderId = 3, PartId = 12},
            };

            settings.ForEach(s => context.Settings.Add(s));
            context.SaveChanges();
        }
    }
}
