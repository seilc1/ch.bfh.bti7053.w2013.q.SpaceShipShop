namespace SpaceShipShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShopItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShopItemAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Item_Id = c.Int(),
                        AttributeType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShopItems", t => t.Item_Id)
                .ForeignKey("dbo.ShopItemAttributeTypes", t => t.AttributeType_Id)
                .Index(t => t.Item_Id)
                .Index(t => t.AttributeType_Id);
            
            CreateTable(
                "dbo.ShopItemAttributeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ShopItemAttributes", new[] { "AttributeType_Id" });
            DropIndex("dbo.ShopItemAttributes", new[] { "Item_Id" });
            DropForeignKey("dbo.ShopItemAttributes", "AttributeType_Id", "dbo.ShopItemAttributeTypes");
            DropForeignKey("dbo.ShopItemAttributes", "Item_Id", "dbo.ShopItems");
            DropTable("dbo.ShopItemAttributeTypes");
            DropTable("dbo.ShopItemAttributes");
            DropTable("dbo.ShopItems");
        }
    }
}
