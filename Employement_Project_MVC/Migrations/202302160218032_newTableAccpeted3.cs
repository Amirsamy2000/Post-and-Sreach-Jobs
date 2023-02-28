namespace Employement_Project_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newTableAccpeted3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accepteds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        AccpetedOn = c.DateTime(nullable: false),
                        PublisherId = c.Int(nullable: false),
                        ApplayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ApplayForJobs", "Accepted_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Accepted_Id", c => c.Int());
            CreateIndex("dbo.ApplayForJobs", "Accepted_Id");
            CreateIndex("dbo.AspNetUsers", "Accepted_Id");
            AddForeignKey("dbo.ApplayForJobs", "Accepted_Id", "dbo.Accepteds", "Id");
            AddForeignKey("dbo.AspNetUsers", "Accepted_Id", "dbo.Accepteds", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Accepted_Id", "dbo.Accepteds");
            DropForeignKey("dbo.ApplayForJobs", "Accepted_Id", "dbo.Accepteds");
            DropIndex("dbo.AspNetUsers", new[] { "Accepted_Id" });
            DropIndex("dbo.ApplayForJobs", new[] { "Accepted_Id" });
            DropColumn("dbo.AspNetUsers", "Accepted_Id");
            DropColumn("dbo.ApplayForJobs", "Accepted_Id");
            DropTable("dbo.Accepteds");
        }
    }
}
