namespace DemoWithUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductsDesc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Desc", c => c.String());
            DropColumn("dbo.Products", "Purchases");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Purchases", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Desc");
        }
    }
}
