namespace SpaceShipShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataContext1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShopItemAttributes", "Item_Id", "dbo.ShopItems");
            DropForeignKey("dbo.ShopItemAttributes", "AttributeType_Id", "dbo.ShopItemAttributeTypes");
            DropIndex("dbo.ShopItemAttributes", new[] { "Item_Id" });
			DropIndex("dbo.ShopItemAttributes", new[] { "AttributeType_Id" });
			DropTable("dbo.ShopItemAttributes");
            CreateTable(
                "dbo.ShopItemAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Item_Id = c.Int(),
                        AttributeType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShopItems", t => t.Item_Id)
                .ForeignKey("dbo.ShopItemAttributeTypes", t => t.AttributeType_Id)
                .Index(t => t.Item_Id)
                .Index(t => t.AttributeType_Id);
            
        }
        
        public override void Down()
		{
			DropTable("dbo.ShopItemAttributes");
            CreateTable(
                "dbo.ShopItemAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Item_Id = c.Int(),
                        AttributeType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.ShopItemAttributes", new[] { "AttributeType_Id" });
            DropIndex("dbo.ShopItemAttributes", new[] { "Item_Id" });
            DropForeignKey("dbo.ShopItemAttributes", "AttributeType_Id", "dbo.ShopItemAttributeTypes");
            DropForeignKey("dbo.ShopItemAttributes", "Item_Id", "dbo.ShopItems");
            CreateIndex("dbo.ShopItemAttributes", "AttributeType_Id");
            CreateIndex("dbo.ShopItemAttributes", "Item_Id");
            AddForeignKey("dbo.ShopItemAttributes", "AttributeType_Id", "dbo.ShopItemAttributeTypes", "Id");
            AddForeignKey("dbo.ShopItemAttributes", "Item_Id", "dbo.ShopItems", "Id");
        }
    }
}
