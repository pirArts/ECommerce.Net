namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyAddressAndOrder : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Addresses", new[] { "User_Id" });
            DropColumn("dbo.Addresses", "UserId");
            RenameColumn(table: "dbo.Addresses", name: "User_Id", newName: "UserId");
            AddColumn("dbo.Orders", "TransactionType", c => c.Int(nullable: false));
            AlterColumn("dbo.Addresses", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Addresses", "UserId");
            DropColumn("dbo.Orders", "TranactionType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "TranactionType", c => c.Int(nullable: false));
            DropIndex("dbo.Addresses", new[] { "UserId" });
            AlterColumn("dbo.Addresses", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "TransactionType");
            RenameColumn(table: "dbo.Addresses", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Addresses", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Addresses", "User_Id");
        }
    }
}
