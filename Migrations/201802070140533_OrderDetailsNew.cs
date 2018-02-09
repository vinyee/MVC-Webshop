namespace DemoWithUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderDetailsNew : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.OrderDetails");
            AddForeignKey("dbo.OrderDetails", "Id", "dbo.OrderDetails");
        }
        
        public override void Down()
        {
        }
    }
}
