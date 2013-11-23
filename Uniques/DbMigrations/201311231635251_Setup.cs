namespace Uniques.DbMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Setup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Loginname = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        PasswordSalt = c.String(nullable: false),
                        Displayname = c.String(),
                        Email = c.String(),
                        LastAction = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAttributeValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, maxLength: 255),
                        AttributeType_Id = c.Int(nullable: false),
                        User_Id = c.Int(),
                        CompleteUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserAttributes", t => t.AttributeType_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserLogins", t => t.User_Id)
                .ForeignKey("dbo.UserLogins", t => t.CompleteUser_Id)
                .Index(t => t.AttributeType_Id)
                .Index(t => t.User_Id)
                .Index(t => t.CompleteUser_Id);
            
            CreateTable(
                "dbo.UserAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TextKey = c.String(maxLength: 30),
                        DefaultText = c.String(maxLength: 30),
                        DefaultDescription = c.String(maxLength: 255),
                        Searchable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileId = c.Guid(nullable: false),
                        Description = c.String(maxLength: 255),
                        IsPortrait = c.Boolean(nullable: false),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserLogins", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Owner_Id", "dbo.UserLogins");
            DropForeignKey("dbo.UserAttributeValues", "CompleteUser_Id", "dbo.UserLogins");
            DropForeignKey("dbo.UserAttributeValues", "User_Id", "dbo.UserLogins");
            DropForeignKey("dbo.UserAttributeValues", "AttributeType_Id", "dbo.UserAttributes");
            DropIndex("dbo.Images", new[] { "Owner_Id" });
            DropIndex("dbo.UserAttributeValues", new[] { "CompleteUser_Id" });
            DropIndex("dbo.UserAttributeValues", new[] { "User_Id" });
            DropIndex("dbo.UserAttributeValues", new[] { "AttributeType_Id" });
            DropTable("dbo.Images");
            DropTable("dbo.UserAttributes");
            DropTable("dbo.UserAttributeValues");
            DropTable("dbo.UserLogins");
        }
    }
}
