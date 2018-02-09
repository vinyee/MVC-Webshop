namespace DemoWithUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prices : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "Cart_UserId", newName: "Carts_UserId");
            RenameIndex(table: "dbo.Products", name: "IX_Cart_UserId", newName: "IX_Carts_UserId");
            AlterColumn("dbo.Products", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Price", c => c.String(nullable: false));
            RenameIndex(table: "dbo.Products", name: "IX_Carts_UserId", newName: "IX_Cart_UserId");
            RenameColumn(table: "dbo.Products", name: "Carts_UserId", newName: "Cart_UserId");
        }
    }
}
