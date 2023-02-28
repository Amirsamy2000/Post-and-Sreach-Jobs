namespace Employement_Project_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newcolForJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "MinPrice", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "MaxPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "MaxPrice");
            DropColumn("dbo.Jobs", "MinPrice");
        }
    }
}
