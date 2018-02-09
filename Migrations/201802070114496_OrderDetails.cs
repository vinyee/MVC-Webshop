namespace DemoWithUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderDetails : DbMigration
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
        }
        
        public override void Down()
        {

        }
    }
}
