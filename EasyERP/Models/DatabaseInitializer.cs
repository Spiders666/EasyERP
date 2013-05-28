using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace EasyERP.Models
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            WebSecurity.InitializeDatabaseConnection("DatabaseContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!Roles.RoleExists("User"))
                Roles.CreateRole("User");

            if (!WebSecurity.UserExists("Admin"))
            {
                WebSecurity.CreateUserAndAccount("Admin", "password");
                Roles.AddUsersToRoles(new[] { "Admin" }, new[] { "Administrator" });
            }

            /* Klienci */

            if (!WebSecurity.UserExists("A"))
            {
                WebSecurity.CreateUserAndAccount("A", "password");
                Roles.AddUsersToRoles(new[] { "A" }, new[] { "User" });
            }
            if (!WebSecurity.UserExists("B"))
            {
                WebSecurity.CreateUserAndAccount("B", "password");
                Roles.AddUsersToRoles(new[] { "B" }, new[] { "User" });
            }
            if (!WebSecurity.UserExists("C"))
            {
                WebSecurity.CreateUserAndAccount("C", "password");
                Roles.AddUsersToRoles(new[] { "C" }, new[] { "User" });
            }

            /* Dostawcy */
            var suppliers = new List<Supplier>
            {
                new Supplier { NIP = "1234567890", Name = "X_NAME", City = "X", ZipCode = "12-123", Street = "X 1", Telephone = "+48 123-123-123", Email = "X@o2.pl", BankAccount = "12345678901234567890123456" },
                new Supplier { NIP = "1234567890", Name = "Y_NAME", City = "Y", ZipCode = "12-123", Street = "Y 2", Telephone = "+48 123-123-123", Email = "Y@o2.pl", BankAccount = "12345678901234567890123456" },
                new Supplier { NIP = "1234567890", Name = "Z_NAME", City = "Z", ZipCode = "12-123", Street = "Z 3", Telephone = "+48 123-123-123", Email = "Z@o2.pl", BankAccount = "12345678901234567890123456" },
            };

            suppliers.ForEach(s => context.Suppliers.Add(s));
            context.SaveChanges();

            /* Rodzaje materiału */
            var materialTypes = new List<MaterialType>
            {
                new MaterialType { Name = "Obicie" },
                new MaterialType { Name = "Wypełnienie" },
                new MaterialType { Name = "Testowy materiał" }
            };

            materialTypes.ForEach(s => context.MaterialTypes.Add(s));
            context.SaveChanges();

            /* Części */
            var materials = new List<Material>
            {
                new Material { SupplierId = 1, MaterialTypeId = 1, Name = "X Obicie 1", Price = 1.0m, Availability = true },
                new Material { SupplierId = 1, MaterialTypeId = 1, Name = "X Obicie 2", Price = 1.0m, Availability = false },
                new Material { SupplierId = 2, MaterialTypeId = 1, Name = "Y Obicie 1", Price = 1.0m, Availability = true },
                new Material { SupplierId = 2, MaterialTypeId = 1, Name = "Y Obicie 2", Price = 1.0m, Availability = true },
                new Material { SupplierId = 3, MaterialTypeId = 1, Name = "Z Obicie 1", Price = 1.0m, Availability = true },
                new Material { SupplierId = 3, MaterialTypeId = 2, Name = "Trociny", Price = 131.0m, Availability = true },
                new Material { SupplierId = 3, MaterialTypeId = 2, Name = "Szmaty", Price = 221.0m, Availability = false },
                new Material { SupplierId = 3, MaterialTypeId = 2, Name = "Bawełna luksusowa", Price = 311.0m, Availability = true }
            };

            materials.ForEach(m => context.Materials.Add(m));
            context.SaveChanges();

            var productTypes = new List<ProductType>()
            {
                new ProductType { Name = "Fotel" },
                new ProductType { Name = "Sofa" },
                new ProductType { Name = "Łóżko" }
            };

            productTypes.ForEach(m => context.ProductTypes.Add(m));
            context.SaveChanges();

            /* Produkty */
            var products = new List<Product>
            {
                new Product { ProductTypeId = 1, Name = "Fotel 1", Description = "opis", Price = 1.0m, Availability = true },
                new Product { ProductTypeId = 1, Name = "Fotel 2", Description = "opis", Price = 1.0m, Availability = false },
                new Product { ProductTypeId = 2, Name = "Sofa 1", Description = "opis", Price = 1.0m, Availability = true },
                new Product { ProductTypeId = 3, Name = "Łóżko 1", Description = "opis", Price = 777.0m, Availability = true }
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();

            /* Konfigurator */
            var configuration = new List<Configuration>
            {
                new Configuration { ProductTypeId = 1, MaterialTypeId = 1 },
                new Configuration { ProductTypeId = 1, MaterialTypeId = 2 },
            };

            configuration.ForEach(p => context.Configurations.Add(p));
            context.SaveChanges();

            var customers = new List<Customer>
            {

                new Customer { Name = "A123456a", SurName="A123456a", City = "A12345a6", ZipCode = "80-800", Street = "A123456", Telephone = "123456789", Email = "stefanszczepan@gmail.com", ActivationLink="", Activation=true, UserId = 2 },
                new Customer { Name = "BA123456", SurName="aA123456", City = "BA123456", ZipCode = "10-200", Street = "1000000", Telephone = "111111111", Email = "blendermaster@gmail.com", ActivationLink="", Activation=true, UserId = 3 },
                new Customer { Name = "CA123456", SurName="aA123456", City = "CA123456", ZipCode = "23-000", Street = "7600000", Telephone = "111111111", Email = "mastablasta@gmail.com", ActivationLink="", Activation=true, UserId = 4 },
            };

            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();

            /* Zamówienia */
            var orders = new List<Order>
            {
                new Order { CustomerId = 1, CreatedAt = DateTime.Now, ProductTypeName = "Fotel", ProductName = "Fotel 1", ProductPrice = 100.00m, State = OrderState.NotConfirmed,  },
                new Order { CustomerId = 2, CreatedAt = DateTime.Now, ProductTypeName = "Sofa", ProductName = "Sofa 2", ProductPrice = 100.00m, State = OrderState.Sent },
                new Order { CustomerId = 3, CreatedAt = DateTime.Now, ProductTypeName = "Fotel", ProductName = "Fotel 2", ProductPrice = 100.00m, State = OrderState.Pending },
                new Order { CustomerId = 3, CreatedAt = DateTime.Now, ProductTypeName = "Sofa", ProductName = "Sofa 1", ProductPrice = 100.00m, State = OrderState.Canceled }
            };

            orders.ForEach(o => context.Orders.Add(o));
            context.SaveChanges();

            /* Pozycje zamówionego zestawu */
            var orderItems = new List<OrderItem>
            {
                new OrderItem { OrderId = 1, MaterialTypeName = "Obicie", MaterialName = "X Obicie 1", Price = 109.99m },
                new OrderItem { OrderId = 1, MaterialTypeName = "Wypełnienie", MaterialName = "Z Wypełnienie 1", Price = 109.99m },

                new OrderItem { OrderId = 2, MaterialTypeName = "Obicie", MaterialName = "X Obicie 1", Price = 109.99m },
                new OrderItem { OrderId = 2, MaterialTypeName = "Wypełnienie", MaterialName = "Z Wypełnienie 1", Price = 109.99m },

                new OrderItem { OrderId = 3, MaterialTypeName = "Obicie", MaterialName = "X Obicie 1", Price = 109.99m },
                new OrderItem { OrderId = 3, MaterialTypeName = "Wypełnienie", MaterialName = "Z Wypełnienie 1", Price = 109.99m },
            };

            orderItems.ForEach(o => context.OrderItems.Add(o));
            context.SaveChanges();
        }
    }
}