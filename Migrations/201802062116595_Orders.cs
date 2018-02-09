namespace DemoWithUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailsId = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.Int(nullable: false),
                        Address = c.Int(nullable: false),
                        PostalCode = c.Int(nullable: false),
                        City = c.String(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailsId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        AppId = c.Int(nullable: false),
                        AppPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderDate = c.Long(nullable: false),
                        UserIdentity = c.String(),
                        OrderTotalPrice = c.Double(nullable: false),
                        OrderDetails_OrderDetailsId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.OrderDetails", t => t.OrderDetails_OrderDetailsId)
                .Index(t => t.OrderDetails_OrderDetailsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderDetails_OrderDetailsId", "dbo.OrderDetails");
            DropIndex("dbo.Orders", new[] { "OrderDetails_OrderDetailsId" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.OrderDetails");
        }
    }
}
