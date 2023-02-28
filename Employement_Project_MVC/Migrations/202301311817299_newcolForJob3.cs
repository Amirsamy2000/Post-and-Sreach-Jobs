namespace Employement_Project_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newcolForJob3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "SreachEngigJob", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "SreachEngigJob", c => c.String(nullable: false));
        }
    }
}
