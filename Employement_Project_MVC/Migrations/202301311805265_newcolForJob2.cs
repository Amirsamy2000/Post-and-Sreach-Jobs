namespace Employement_Project_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newcolForJob2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "SreachEngigJob", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "SreachEngigJob");
        }
    }
}
