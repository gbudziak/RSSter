namespace DBContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserChannelID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserItems", "UserChannel_Id", "dbo.UserChannels");
            DropIndex("dbo.UserItems", new[] { "UserChannel_Id" });
            RenameColumn(table: "dbo.UserItems", name: "UserChannel_Id", newName: "UserChannelId");
            AlterColumn("dbo.UserItems", "UserChannelId", c => c.Long(nullable: false));
            CreateIndex("dbo.UserItems", "UserChannelId");
            AddForeignKey("dbo.UserItems", "UserChannelId", "dbo.UserChannels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserItems", "UserChannelId", "dbo.UserChannels");
            DropIndex("dbo.UserItems", new[] { "UserChannelId" });
            AlterColumn("dbo.UserItems", "UserChannelId", c => c.Long());
            RenameColumn(table: "dbo.UserItems", name: "UserChannelId", newName: "UserChannel_Id");
            CreateIndex("dbo.UserItems", "UserChannel_Id");
            AddForeignKey("dbo.UserItems", "UserChannel_Id", "dbo.UserChannels", "Id");
        }
    }
}
