namespace RssDataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserSubscriptions", "SubscribedUserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserSubscriptions", "SubscribedUserId", c => c.Long(nullable: false));
        }
    }
}
