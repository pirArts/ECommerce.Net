namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCart_Order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Street = c.String(),
                        ZipCode = c.String(),
                        UserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CartId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CartId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        State = c.Int(nullable: false),
                        TranactionType = c.Int(nullable: false),
                        ShipFee = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Products", "Thumbnail", c => c.String());
            AddColumn("dbo.Products", "Stock", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "SalesCount", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "CreateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Products", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImageUrl", c => c.String());
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CartItems", "CartId", "dbo.Carts");
            DropForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Addresses", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropIndex("dbo.CartItems", new[] { "ProductId" });
            DropIndex("dbo.CartItems", new[] { "CartId" });
            DropIndex("dbo.Addresses", new[] { "User_Id" });
            DropColumn("dbo.Products", "CreateTime");
            DropColumn("dbo.Products", "SalesCount");
            DropColumn("dbo.Products", "Stock");
            DropColumn("dbo.Products", "Thumbnail");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Carts");
            DropTable("dbo.CartItems");
            DropTable("dbo.Addresses");
        }
    }
}
