namespace Employement_Project_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newColAplayForJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplayForJobs", "CvApplay", c => c.String());
            AddColumn("dbo.ApplayForJobs", "LinksGlary", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplayForJobs", "LinksGlary");
            DropColumn("dbo.ApplayForJobs", "CvApplay");
        }
    }
}
