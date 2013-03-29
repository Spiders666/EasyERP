﻿using System;
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
                new Customer { FirstName = "A", LastName = "A" },
                new Customer { FirstName = "B", LastName = "B" },
                new Customer { FirstName = "C", LastName = "C" },
            };

            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();

            /* Dostawcy */
            var suppliers = new List<Supplier>
            {
                new Supplier { Name = "X" },
                new Supplier { Name = "Y" },
                new Supplier { Name = "Z" }
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
                new Part { SuppierId = 1, TypetId = 1, Name = "XObudowa1", Price = 1.0m },
                new Part { SuppierId = 1, TypetId = 1, Name = "XObudowa2", Price = 1.0m  },
                new Part { SuppierId = 1, TypetId = 2, Name = "XWkład1", Price = 1.0m  },

                new Part { SuppierId = 2, TypetId = 1, Name = "YObudowa1", Price = 1.0m  },
                new Part { SuppierId = 2, TypetId = 2, Name = "YWkład1", Price = 1.0m  },
                new Part { SuppierId = 2, TypetId = 2, Name = "YWkład2", Price = 1.0m  },
                new Part { SuppierId = 2, TypetId = 2, Name = "YWkład3", Price = 1.0m  },

                new Part { SuppierId = 3, TypetId = 3, Name = "ZSilnik1", Price = 1.0m  },
                new Part { SuppierId = 3, TypetId = 3, Name = "ZSilnik2", Price = 1.0m  },
                new Part { SuppierId = 3, TypetId = 4, Name = "ZOpony1", Price = 1.0m  },
                new Part { SuppierId = 3, TypetId = 4, Name = "ZOpony2", Price = 1.0m  },
                new Part { SuppierId = 3, TypetId = 4, Name = "ZOpony3", Price = 1.0m  }
            };

            parts.ForEach(p => context.Parts.Add(p));
            context.SaveChanges();

            /* Zamówienia */
            var orders = new List<Order>
            {
                new Order { CustomerId = 1, CreatedAt = DateTime.Now },
                new Order { CustomerId = 2, CreatedAt = DateTime.Now },
                new Order { CustomerId = 3, CreatedAt = DateTime.Now }
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