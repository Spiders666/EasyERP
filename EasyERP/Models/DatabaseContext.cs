using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace EasyERP.Models
{

    public class DatabaseContext : DbContext
    {
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<Material> Materials { get; set; }

        public DbSet<Configuration> Configurations { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
