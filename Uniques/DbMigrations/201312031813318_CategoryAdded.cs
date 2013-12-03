namespace Uniques.DbMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserAttributeValues", "AttributeType_Id", "dbo.UserAttributes");
            DropIndex("dbo.UserAttributeValues", new[] { "AttributeType_Id" });
            CreateTable(
                "dbo.UserAttributeCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TextKey = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Translations",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Lcid = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => new { t.Key, t.Lcid });
            
            AddColumn("dbo.UserAttributes", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserAttributeValues", "AttributeType_Id", c => c.Int());
            CreateIndex("dbo.UserAttributes", "CategoryId");
            CreateIndex("dbo.UserAttributeValues", "AttributeType_Id");
            AddForeignKey("dbo.UserAttributes", "CategoryId", "dbo.UserAttributeCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserAttributeValues", "AttributeType_Id", "dbo.UserAttributes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAttributeValues", "AttributeType_Id", "dbo.UserAttributes");
            DropForeignKey("dbo.UserAttributes", "CategoryId", "dbo.UserAttributeCategories");
            DropIndex("dbo.UserAttributeValues", new[] { "AttributeType_Id" });
            DropIndex("dbo.UserAttributes", new[] { "CategoryId" });
            AlterColumn("dbo.UserAttributeValues", "AttributeType_Id", c => c.Int(nullable: false));
            DropColumn("dbo.UserAttributes", "CategoryId");
            DropTable("dbo.Translations");
            DropTable("dbo.UserAttributeCategories");
            CreateIndex("dbo.UserAttributeValues", "AttributeType_Id");
            AddForeignKey("dbo.UserAttributeValues", "AttributeType_Id", "dbo.UserAttributes", "Id", cascadeDelete: true);
        }
    }
}
