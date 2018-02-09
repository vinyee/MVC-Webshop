namespace DemoWithUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.UserId);
            
            AddColumn("dbo.Products", "Cart_UserId", c => c.Int());
            CreateIndex("dbo.Products", "Cart_UserId");
            AddForeignKey("dbo.Products", "Cart_UserId", "dbo.Carts", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Cart_UserId", "dbo.Carts");
            DropIndex("dbo.Products", new[] { "Cart_UserId" });
            DropColumn("dbo.Products", "Cart_UserId");
            DropTable("dbo.Carts");
        }
    }
}
