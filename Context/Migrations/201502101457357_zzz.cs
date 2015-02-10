namespace RssDataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zzz : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UsersHistories", newName: "UserHistories");
            AlterColumn("dbo.UserHistories", "ActionName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserHistories", "ActionName", c => c.Int(nullable: false));
            RenameTable(name: "dbo.UserHistories", newName: "UsersHistories");
        }
    }
}
