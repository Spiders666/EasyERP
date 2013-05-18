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

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!Roles.RoleExists("Administrator"))
            {
                Roles.CreateRole("Administrator");
            }

            if (!Roles.RoleExists("User"))
            {
                Roles.CreateRole("User");
            }

            if (!WebSecurity.UserExists("Admin"))
            {
                WebSecurity.CreateUserAndAccount("Admin", "password");
                Roles.AddUsersToRoles(new[] { "Admin" }, new[] { "Administrator" });
            }

            /* Klienci */
            var customers = new List<Customer>
            {

                new Customer { Name = "A", City = "A", ZipCode = "A", Street = "A", Telephone = "A", Email = "A", UserId = 2 },
                new Customer { Name = "B", City = "B", ZipCode = "B", Street = "B", Telephone = "B", Email = "B" },
                new Customer { Name = "C", City = "C", ZipCode = "C", Street = "C", Telephone = "C", Email = "C" },
                new Customer { Name = "C", City = "C", ZipCode = "C", Street = "C", Telephone = "C", Email = "C" },
                new Customer { Name = "C", City = "C", ZipCode = "C", Street = "C", Telephone = "C", Email = "C" },
                new Customer { Name = "C", City = "C", ZipCode = "C", Street = "C", Telephone = "C", Email = "C" },
            };

            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();

            /* Zamówienia */
            var orders = new List<Order>
            {
                new Order { CustomerId = 1, CreatedAt = DateTime.Now, ProductName = "Fotel 1", ProductPrice = 100.00m, ProductType = ProductType.ARMCHAIR, State = OrderState.NOT_CONFIRMED,  },
                new Order { CustomerId = 2, CreatedAt = DateTime.Now, ProductName = "Sofa 2", ProductPrice = 100.00m, ProductType = ProductType.SOFA, State = OrderState.SENT },
                new Order { CustomerId = 3, CreatedAt = DateTime.Now, ProductName = "Fotel 2", ProductPrice = 100.00m, ProductType = ProductType.ARMCHAIR, State = OrderState.PENDING }
            };

            orders.ForEach(o => context.Orders.Add(o));
            context.SaveChanges();

            /* Pozycje zamówionego zestawu */
            var orderItems = new List<OrderItem>
            {
                new OrderItem { OrderId = 1, MaterialName = "X Obicie 1", Price = 109.99m, MaterialType = MaterialType.UPHOLSTERY },
                new OrderItem { OrderId = 1, MaterialName = "Z Wypełnienie 1", Price = 109.99m, MaterialType = MaterialType.FILL },

                new OrderItem { OrderId = 2, MaterialName = "X Obicie 1", Price = 109.99m, MaterialType = MaterialType.UPHOLSTERY },
                new OrderItem { OrderId = 2, MaterialName = "Z Wypełnienie 1", Price = 109.99m, MaterialType = MaterialType.FILL },

                new OrderItem { OrderId = 3, MaterialName = "X Obicie 1", Price = 109.99m, MaterialType = MaterialType.UPHOLSTERY },
                new OrderItem { OrderId = 3, MaterialName = "Z Wypełnienie 1", Price = 109.99m, MaterialType = MaterialType.FILL },
            };

            orderItems.ForEach(o => context.OrderItems.Add(o));
            context.SaveChanges();

            /* Dostawcy */
            var suppliers = new List<Supplier>
            {
                new Supplier { NIP = "1234567890", Name = "X_NAME", City = "X", ZipCode = "12-123", Street = "X 1", Telephone = "+48 123-123-123", Email = "X@o2.pl", BankAccount = "12345678901234567890123456" },
                new Supplier { NIP = "1234567890", Name = "Y_NAME", City = "Y", ZipCode = "12-123", Street = "Y 2", Telephone = "+48 123-123-123", Email = "Y@o2.pl", BankAccount = "12345678901234567890123456" },
                new Supplier { NIP = "1234567890", Name = "Z_NAME", City = "Z", ZipCode = "12-123", Street = "Z 3", Telephone = "+48 123-123-123", Email = "Z@o2.pl", BankAccount = "12345678901234567890123456" },
            };

            suppliers.ForEach(s => context.Suppliers.Add(s));
            context.SaveChanges();

            /* Części */
            var materials = new List<Material>
            {
                new Material { SupplierId = 1, Type = MaterialType.UPHOLSTERY, Name = "X Obicie 1", Price = 1.0m, Availability = true },
                new Material { SupplierId = 1, Type = MaterialType.UPHOLSTERY, Name = "X Obicie 2", Price = 1.0m, Availability = false },
                new Material { SupplierId = 2, Type = MaterialType.UPHOLSTERY, Name = "Y Obicie 1", Price = 1.0m, Availability = true },
                new Material { SupplierId = 2, Type = MaterialType.UPHOLSTERY, Name = "Y Obicie 2", Price = 1.0m, Availability = true },
                new Material { SupplierId = 3, Type = MaterialType.UPHOLSTERY, Name = "Z Obicie 1", Price = 1.0m, Availability = true },
                new Material { SupplierId = 3, Type = MaterialType.FILL, Name = "Trociny", Price = 131.0m, Availability = true },
                new Material { SupplierId = 3, Type = MaterialType.FILL, Name = "Szmaty", Price = 221.0m, Availability = false },
                new Material { SupplierId = 3, Type = MaterialType.FILL, Name = "Bawełna luksusowa", Price = 311.0m, Availability = true }
            };

            materials.ForEach(m => context.Materials.Add(m));
            context.SaveChanges();

            /* Produkty */
            var products = new List<Product>
            {
                new Product { Type = ProductType.ARMCHAIR, Name = "Fotel 1", Price = 1.0m, Availability = true },
                new Product { Type = ProductType.ARMCHAIR, Name = "Fotel 2", Price = 1.0m, Availability = false },
                new Product { Type = ProductType.SOFA, Name = "Sofa 1", Price = 1.0m, Availability = true },
                new Product { Type = ProductType.BED, Name = "Łóżko 1", Price = 777.0m, Availability = true }
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }
    }
}