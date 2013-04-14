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

            /* Zamówienia */
            var orders = new List<Order>
            {
                new Order { CustomerId = 1, CreatedAt = DateTime.Now },
                new Order { CustomerId = 2, CreatedAt = DateTime.Now },
                new Order { CustomerId = 3, CreatedAt = DateTime.Now }
            };

            orders.ForEach(o => context.Orders.Add(o));
            context.SaveChanges();

            /* Pozycje zamówionego zestawu */
            var orderItems = new List<OrderItem>
            {
                new OrderItem { OrderId = 1, Name = "Nazwa_Produktu", Price = 109.99m },
                new OrderItem { OrderId = 1, Name = "Nazwa_Części", Price = 109.99m },

                new OrderItem { OrderId = 2, Name = "Nazwa_Produktu", Price = 109.99m },
                new OrderItem { OrderId = 2, Name = "Nazwa_Części", Price = 109.99m },

                new OrderItem { OrderId = 3, Name = "Nazwa_Produktu", Price = 109.99m },
                new OrderItem { OrderId = 3, Name = "Nazwa_Części", Price = 109.99m },
            };

            orderItems.ForEach(o => context.OrderItems.Add(o));
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

            /* Części */
            var parts = new List<Part>
            {
                new Part { SupplierId = 1, Type = Part.TYPE_UPHOLSTERY, Name = "X Obicie 1", Price = 1.0m, Availability = true },
                new Part { SupplierId = 1, Type = Part.TYPE_UPHOLSTERY, Name = "X Obicie 2", Price = 1.0m, Availability = false },
                new Part { SupplierId = 2, Type = Part.TYPE_UPHOLSTERY, Name = "Y Obicie 1", Price = 1.0m, Availability = true },
                new Part { SupplierId = 2, Type = Part.TYPE_UPHOLSTERY, Name = "Y Obicie 2", Price = 1.0m, Availability = true },
                new Part { SupplierId = 3, Type = Part.TYPE_UPHOLSTERY, Name = "Z Obicie 1", Price = 1.0m, Availability = true },
                new Part { SupplierId = 3, Type = Part.TYPE_UPHOLSTERY, Name = "Z Obicie 2", Price = 1.0m, Availability = true }
            };

            parts.ForEach(p => context.Parts.Add(p));
            context.SaveChanges();

            /* Produkty */
            var products = new List<Product>
            {
                new Product { Type = Product.TYPE_CHAIR, Name = "Fotel 1", Price = 1.0m, Availability = false },
                new Product { Type = Product.TYPE_CHAIR, Name = "Fotel 2", Price = 1.0m, Availability = false }
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();

            /* Ustawienia produktów */
            var settings = new List<Setting>
            {
                new Setting { ProductId = 1, PartId = 1 },
                new Setting { ProductId = 2, PartId = 3 },
            };

            settings.ForEach(s => context.Settings.Add(s));
            context.SaveChanges();
        }
    }
}
