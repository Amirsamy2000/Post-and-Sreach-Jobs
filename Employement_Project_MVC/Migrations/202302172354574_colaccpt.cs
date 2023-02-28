namespace Employement_Project_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class colaccpt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplayForJobs", "Accept", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplayForJobs", "Accept");
        }
    }
}
