namespace Employement_Project_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newIid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "UaerID", c => c.String());
            AddColumn("dbo.Jobs", "user_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Jobs", "user_Id");
            AddForeignKey("dbo.Jobs", "user_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "user_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Jobs", new[] { "user_Id" });
            DropColumn("dbo.Jobs", "user_Id");
            DropColumn("dbo.Jobs", "UaerID");
        }
    }
}
