namespace DemoWithUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewOrderDetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "OrderId", "dbo.Orders");
            AddForeignKey("dbo.Orders", "Id", "dbo.Orders");
        }
        
        public override void Down()
        {
        }
    }
}
