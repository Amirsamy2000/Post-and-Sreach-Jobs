namespace Employement_Project_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newclolregister : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneVisitor", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "PathImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PathImage");
            DropColumn("dbo.AspNetUsers", "PhoneVisitor");
            DropColumn("dbo.AspNetUsers", "Country");
        }
    }
}
