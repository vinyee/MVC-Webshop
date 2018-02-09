namespace DemoWithUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdersDetails : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetails", "Lastname", c => c.String(nullable: false));
            AlterColumn("dbo.OrderDetails", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetails", "Address", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDetails", "Lastname", c => c.Int(nullable: false));
        }
    }
}
