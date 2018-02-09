namespace DemoWithUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ordersss : DbMigration
    {
        public override void Up()
        {


            AddColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: false));


        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Orders", "Id");
            AddPrimaryKey("dbo.Orders", "OrderId");
        }
    }
}
