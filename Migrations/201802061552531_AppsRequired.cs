namespace DemoWithUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppsRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Price", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ImageUrl", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Desc", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Desc", c => c.String());
            AlterColumn("dbo.Products", "ImageUrl", c => c.String());
            AlterColumn("dbo.Products", "Price", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
        }
    }
}
