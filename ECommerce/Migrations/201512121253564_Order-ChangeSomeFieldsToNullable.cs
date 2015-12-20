namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderChangeSomeFieldsToNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Orders", new[] { "AddressId" });
            AlterColumn("dbo.Orders", "State", c => c.Int());
            AlterColumn("dbo.Orders", "TransactionType", c => c.Int());
            AlterColumn("dbo.Orders", "ShipFee", c => c.Single());
            AlterColumn("dbo.Orders", "AddressId", c => c.Int());
            CreateIndex("dbo.Orders", "AddressId");
            AddForeignKey("dbo.Orders", "AddressId", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Orders", new[] { "AddressId" });
            AlterColumn("dbo.Orders", "AddressId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "ShipFee", c => c.Single(nullable: false));
            AlterColumn("dbo.Orders", "TransactionType", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "State", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "AddressId");
            AddForeignKey("dbo.Orders", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
    }
}
