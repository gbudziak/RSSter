namespace RssDataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        PreferredView_Id = c.Long(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.UserCustomViews", t => t.PreferredView_Id)
                .Index(t => t.UserId)
                .Index(t => t.PreferredView_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfoes", "PreferredView_Id", "dbo.UserCustomViews");
            DropForeignKey("dbo.UserInfoes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserInfoes", new[] { "PreferredView_Id" });
            DropIndex("dbo.UserInfoes", new[] { "UserId" });
            DropTable("dbo.UserInfoes");
        }
    }
}
