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
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DatabaseContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            if (!Roles.RoleExists(UserRole.Administrator))
                Roles.CreateRole(UserRole.Administrator);

            if (!Roles.RoleExists(UserRole.User))
                Roles.CreateRole(UserRole.User);

            if (!WebSecurity.UserExists("Admin"))
            {
                WebSecurity.CreateUserAndAccount("Admin", "password");
                Roles.AddUserToRole("Admin", UserRole.Administrator);
            }

            /* Klienci */

            if (!WebSecurity.UserExists("Abc"))
            {
                WebSecurity.CreateUserAndAccount("Abc", "password");
                Roles.AddUserToRole("Abc", UserRole.User);
            }
            if (!WebSecurity.UserExists("Bbc"))
            {
                WebSecurity.CreateUserAndAccount("Bbc", "password");
                Roles.AddUserToRole("Bbc", UserRole.User);
            }
            if (!WebSecurity.UserExists("Cbc"))
            {
                WebSecurity.CreateUserAndAccount("Cbc", "password");
                Roles.AddUserToRole("Cbc", UserRole.User);
            }

            /* Dostawcy */
            var suppliers = new List<Supplier>
            {
                new Supplier { NIP = "1234567890", Name = "Intel", City = "Sopot", ZipCode = "12-123", Street = "Akacjowa 23", Telephone = "+48 123-123-123", Email = "hej@intel.pl", BankAccount = "12345678901234567890123456" },
                new Supplier { NIP = "1234567890", Name = "Cisco", City = "Gdańsk", ZipCode = "12-123", Street = "Wspaniała 30", Telephone = "+48 123-123-123", Email = "spam@cisco.pl", BankAccount = "12345678901234567890123456" },
                new Supplier { NIP = "1234567890", Name = "Microsoft", City = "Gdynia", ZipCode = "12-123", Street = "Niegrzeczna 10", Telephone = "+48 123-123-123", Email = "email@microsoft.pl", BankAccount = "12345678901234567890123456" },
            };

            suppliers.ForEach(s => context.Suppliers.Add(s));
            context.SaveChanges();

            /* Rodzaje materiału */
            var materialTypes = new List<MaterialType>
            {
                new MaterialType { Name = "Obicie" },
                new MaterialType { Name = "Wypełnienie" },
            };

            materialTypes.ForEach(s => context.MaterialTypes.Add(s));
            context.SaveChanges();

            /* Części */
            var materials = new List<Material>
            {
                new Material { SupplierId = 1, TypeId = 1, Name = "Skóra sztuczna", Price = 100.0m, ImageName="1.jpeg", Availability = true },
                new Material { SupplierId = 1, TypeId = 1, Name = "Skóra naturalna", Price = 500.0m, ImageName="2.jpeg", Availability = false },
                new Material { SupplierId = 2, TypeId = 1, Name = "Szmaty", Price = 10.0m, ImageName="3.jpg", Availability = true },
                new Material { SupplierId = 2, TypeId = 1, Name = "Materiał z chin", Price = 50.0m, ImageName="4.jpg", Availability = true },
                new Material { SupplierId = 3, TypeId = 1, Name = "Bawełna", Price = 300.0m, ImageName="5.jpg", Availability = true },
                new Material { SupplierId = 3, TypeId = 2, Name = "Trociny", Price = 10.0m, ImageName="6.jpg", Availability = true },
                new Material { SupplierId = 3, TypeId = 2, Name = "Szmaty", Price = 25.0m, ImageName="7.png", Availability = false },
                new Material { SupplierId = 3, TypeId = 2, Name = "Materac", Price = 50.0m, ImageName="8.png", Availability = true }
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
                new Product { TypeId = 1, Name = "Niagara", Description = "Mamy dla Państwa w specjalnej ofercie fotel NIAGARA. \n Elegancki i stylowy fotel . Piękne wykończenia, starannie dobrane kolory oraz niezwykły szyk powoduje, że idealnie nadaje się do salonu.", ImageName="1.jpg", Price = 600.0m, Availability = true },
                new Product { TypeId = 1, Name = "Leonardo Lux", Description = "Fotel to niepowtarzalny element całej aranżacji mebli w Twoim pokoju. Model dostępny w skórze naturalnej oraz tkaninie.", ImageName="2.jpg", Price = 450.0m, Availability = false },
                new Product { TypeId = 2, Name = "Sofa", Description = "Fotele i sofy z kolekcji Holly są lekkie, wytrzymałe i niewielkie rozmiarowo. Dzięki temu można wykorzystać je do aranżacji nawet w niedużych przestrzeniach. Jednak ich największą zaletą jest możliwość rozłożenia zarówno kanapy, jak i fotela, dzięki czemu można na nich spać. \n Wielofunkcyjne meble z kolekcji „Holly” idealnie nadają się do aranżacji niewielkich pomieszczeń, szczególnie tych, w których pełnić mają podwójną rolę – reprezentatywną oraz miejsca do spania. Dzięki klasycznemu projektowi wykorzystać je można zarówno w zaciszu domowym, jak i oficjalnych pomieszczeniach.", ImageName="3.jpg", Price = 1200.0m, Availability = true },
                new Product { TypeId = 3, Name = "Benitor", Description = "Łóżko Benitor to jeden z pierwszych naszych projektów. Charakterystyczne solidne nogi o przekroju litery L nadają łóżku cechy wyrobu bardzo statecznego i solidnego choć wizualnie całość sprawia wrażenie lekkiego. Mimo upływu czasu, model ten nadal cieszy się dużą popularnością szczególnie wśród odbiorców o nowoczesnym guście. Co ciekawe, również użytkownicy łóżka Benitor którzy mają małe dzieci podkreślają jak istotną rolę odgrywają wysokość oraz krągłości krawędzi w konfrontacji z małymi exploratorami. \n Jak powszechnie wiadomo drewno bukowe charakteryzuje się bardzo dużą wytrzymałością. Mebel dedykowany dla wszystkich którzy preferują solidne produkty. Sposób wykonania, sprawia, że łóżko jak najbardziej może być użytkowane przez najcięższe osoby.",  ImageName="4.jpg", Price = 777.0m, Availability = true }
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

            /* Klienci */

            var customers = new List<Customer>
            {

                new Customer { Name = "Stefan", SurName="Specjalny", City = "Rumia", ZipCode = "80-800", Street = "ul. Jarzębinowa", Telephone = "123456789", Email = "stefanszczepan@gmail.com", ActivationLink="", Activation=true, UserId = 2 },
                new Customer { Name = "Szczepan", SurName="Niezwykły", City = "Warszawa", ZipCode = "10-200", Street = "ul. Galaktyczna", Telephone = "123456789", Email = "blendermaster@gmail.com", ActivationLink="", Activation=true, UserId = 3 },
                new Customer { Name = "Zbyszek", SurName="Kac", City = "Zakopane", ZipCode = "23-000", Street = "ul. Akacjowa", Telephone = "123456789", Email = "mastablasta@gmail.com", ActivationLink="", Activation=true, UserId = 4 },
            };

            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();

            /* Zamówienia */
            var orders = new List<Order>
            {
                new Order { CustomerId = 1, CreatedAt = DateTime.Now, ProductTypeName = "Fotel", ProductName = "Fotel Masakra", ProductPrice = 100.00m, State = OrderState.NotConfirmed,  },
                new Order { CustomerId = 2, CreatedAt = DateTime.Now, ProductTypeName = "Sofa", ProductName = "Sofa Royal", ProductPrice = 100.00m, State = OrderState.Sent },
                new Order { CustomerId = 3, CreatedAt = DateTime.Now, ProductTypeName = "Fotel", ProductName = "Fotel Zwyczajny", ProductPrice = 100.00m, State = OrderState.Pending },
                new Order { CustomerId = 3, CreatedAt = DateTime.Now, ProductTypeName = "Sofa", ProductName = "Sofa ", ProductPrice = 100.00m, State = OrderState.Canceled }
            };

            orders.ForEach(o => context.Orders.Add(o));
            context.SaveChanges();

            /* Pozycje zamówionego zestawu */
            var orderItems = new List<OrderItem>
            {
                new OrderItem { OrderId = 1, MaterialTypeName = "Obicie", MaterialName = "Skóra sztuczna", Price = 100.00m },
                new OrderItem { OrderId = 1, MaterialTypeName = "Wypełnienie", MaterialName = "Słoma", Price = 10.00m },

                new OrderItem { OrderId = 2, MaterialTypeName = "Obicie", MaterialName = "Skóra z niedzwiedzia", Price = 10000.00m },
                new OrderItem { OrderId = 2, MaterialTypeName = "Wypełnienie", MaterialName = "Bawełna zmieszana ze złotem", Price = 1000.00m },

                new OrderItem { OrderId = 3, MaterialTypeName = "Obicie", MaterialName = "Skóra naturnalna", Price = 450.00m },
                new OrderItem { OrderId = 3, MaterialTypeName = "Wypełnienie", MaterialName = "Puch bawełniany", Price = 1000.00m },
            };

            orderItems.ForEach(o => context.OrderItems.Add(o));
            context.SaveChanges();
        }
    }
}