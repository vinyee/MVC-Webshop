namespace DemoWithUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orderssss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: false));
        }
        
        public override void Down()
        {
        }
    }
}
