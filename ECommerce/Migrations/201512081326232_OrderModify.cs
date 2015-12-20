namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderModify : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CreateTime");
        }
    }
}
