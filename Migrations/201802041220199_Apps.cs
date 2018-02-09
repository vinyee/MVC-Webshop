namespace DemoWithUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Apps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Purchases", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Purchases");
        }
    }
}
