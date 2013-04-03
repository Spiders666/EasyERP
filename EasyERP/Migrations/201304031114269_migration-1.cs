namespace EasyERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Type",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Part",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        Availability = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Supplier", t => t.SupplierId, cascadeDelete: true)
                .ForeignKey("dbo.Type", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NIP = c.String(),
                        Name = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                        Street = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                        BankAccount = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Setting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        PartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Part", t => t.PartId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.PartId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                        Street = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Order", new[] { "CustomerId" });
            DropIndex("dbo.Setting", new[] { "PartId" });
            DropIndex("dbo.Setting", new[] { "OrderId" });
            DropIndex("dbo.Part", new[] { "TypeId" });
            DropIndex("dbo.Part", new[] { "SupplierId" });
            DropIndex("dbo.Type", new[] { "ProductId" });
            DropForeignKey("dbo.Order", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Setting", "PartId", "dbo.Part");
            DropForeignKey("dbo.Setting", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Part", "TypeId", "dbo.Type");
            DropForeignKey("dbo.Part", "SupplierId", "dbo.Supplier");
            DropForeignKey("dbo.Type", "ProductId", "dbo.Product");
            DropTable("dbo.Customer");
            DropTable("dbo.Order");
            DropTable("dbo.Setting");
            DropTable("dbo.Supplier");
            DropTable("dbo.Part");
            DropTable("dbo.Type");
            DropTable("dbo.Product");
        }
    }
}
