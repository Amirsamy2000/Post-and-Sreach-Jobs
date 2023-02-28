namespace Employement_Project_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addApplayJob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplayForJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        AplayOn = c.DateTime(nullable: false),
                        JobId = c.Int(nullable: false),
                        Userid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Userid)
                .Index(t => t.JobId)
                .Index(t => t.Userid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplayForJobs", "Userid", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplayForJobs", "JobId", "dbo.Jobs");
            DropIndex("dbo.ApplayForJobs", new[] { "Userid" });
            DropIndex("dbo.ApplayForJobs", new[] { "JobId" });
            DropTable("dbo.ApplayForJobs");
        }
    }
}
