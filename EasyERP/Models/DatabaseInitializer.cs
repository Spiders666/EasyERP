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
                new Order { CustomerId = 1, CreatedAt = DateTime.Now, ProductName = "Fotel 1", ProductPrice = 100.00m, ProductType = Product.Types.CHAIR },
                new Order { CustomerId = 2, CreatedAt = DateTime.Now, ProductName = "Sofa 2", ProductPrice = 100.00m, ProductType = Product.Types.SOFA },
                new Order { CustomerId = 3, CreatedAt = DateTime.Now, ProductName = "Fotel 2", ProductPrice = 100.00m, ProductType = Product.Types.CHAIR }
            };

            orders.ForEach(o => context.Orders.Add(o));
            context.SaveChanges();

            /* Pozycje zamówionego zestawu */
            var orderItems = new List<OrderItem>
            {
                new OrderItem { OrderId = 1, MaterialName = "X Obicie 1", Price = 109.99m, MaterialType = Material.Types.UPHOLSTERY },
                new OrderItem { OrderId = 1, MaterialName = "Z Wypełnienie 1", Price = 109.99m, MaterialType = Material.Types.FILL },

                new OrderItem { OrderId = 2, MaterialName = "X Obicie 1", Price = 109.99m, MaterialType = Material.Types.UPHOLSTERY },
                new OrderItem { OrderId = 2, MaterialName = "Z Wypełnienie 1", Price = 109.99m, MaterialType = Material.Types.FILL },

                new OrderItem { OrderId = 3, MaterialName = "X Obicie 1", Price = 109.99m, MaterialType = Material.Types.UPHOLSTERY },
                new OrderItem { OrderId = 3, MaterialName = "Z Wypełnienie 1", Price = 109.99m, MaterialType = Material.Types.FILL },
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
            var materials = new List<Material>
            {
                new Material { SupplierId = 1, Type = Material.Types.UPHOLSTERY, Name = "X Obicie 1", Price = 1.0m, Availability = true },
                new Material { SupplierId = 1, Type = Material.Types.UPHOLSTERY, Name = "X Obicie 2", Price = 1.0m, Availability = false },
                new Material { SupplierId = 2, Type = Material.Types.UPHOLSTERY, Name = "Y Obicie 1", Price = 1.0m, Availability = true },
                new Material { SupplierId = 2, Type = Material.Types.UPHOLSTERY, Name = "Y Obicie 2", Price = 1.0m, Availability = true },
                new Material { SupplierId = 3, Type = Material.Types.UPHOLSTERY, Name = "Z Obicie 1", Price = 1.0m, Availability = true },
                new Material { SupplierId = 3, Type = Material.Types.UPHOLSTERY, Name = "Z Obicie 2", Price = 1.0m, Availability = true },
                new Material { SupplierId = 3, Type = Material.Types.UPHOLSTERY, Name = "Z Wypełnienie 1", Price = 1.0m, Availability = true }
            };

            materials.ForEach(m => context.Materials.Add(m));
            context.SaveChanges();

            /* Produkty */
            var products = new List<Product>
            {
                new Product { Type = Product.Types.CHAIR, Name = "Fotel 1", Price = 1.0m, Availability = true },
                new Product { Type = Product.Types.CHAIR, Name = "Fotel 2", Price = 1.0m, Availability = true },
                new Product { Type = Product.Types.CHAIR, Name = "Sofa 1", Price = 1.0m, Availability = true },
                new Product { Type = Product.Types.CHAIR, Name = "Sofa 2", Price = 777.0m, Availability = false }
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }
    }
}
