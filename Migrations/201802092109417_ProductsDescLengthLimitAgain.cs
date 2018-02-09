namespace DemoWithUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductsDescLengthLimitAgain : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Desc", c => c.String(nullable: false, maxLength: 35));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Desc", c => c.String(nullable: false, maxLength: 25));
        }
    }
}
