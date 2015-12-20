namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttendanceAndVacation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Time = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Vacations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ApproverId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApproverId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ApproverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vacations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vacations", "ApproverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attendances", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Vacations", new[] { "ApproverId" });
            DropIndex("dbo.Vacations", new[] { "UserId" });
            DropIndex("dbo.Attendances", new[] { "UserId" });
            DropTable("dbo.Vacations");
            DropTable("dbo.Attendances");
        }
    }
}
