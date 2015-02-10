namespace RssDataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSubscriptions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserEmail = c.String(),
                        SubscribedUserId = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSubscriptions", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserSubscriptions", new[] { "ApplicationUserId" });
            DropTable("dbo.UserSubscriptions");
        }
    }
}
