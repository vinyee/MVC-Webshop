namespace DemoWithUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewOrders : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: false));
            AddPrimaryKey("dbo.Orders", "OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false));
        }
    }
}
