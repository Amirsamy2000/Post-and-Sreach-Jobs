namespace Employement_Project_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newcolForJob4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "SreachEngigJob", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "SreachEngigJob", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
