namespace Employement_Project_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upadecol : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accepteds", "PublisherId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accepteds", "PublisherId", c => c.Int(nullable: false));
        }
    }
}
