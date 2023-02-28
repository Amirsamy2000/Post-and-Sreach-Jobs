namespace Employement_Project_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Jobs", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "MyProperty", c => c.Int(nullable: false));
        }
    }
}
