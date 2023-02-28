namespace Employement_Project_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdatatypephone2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "PhoneVisitor", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "PhoneVisitor", c => c.Int(nullable: false));
        }
    }
}
