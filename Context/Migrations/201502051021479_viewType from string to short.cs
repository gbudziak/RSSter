namespace RssDataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class viewTypefromstringtoshort : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserCustomViews", "ViewType", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserCustomViews", "ViewType", c => c.String());
        }
    }
}
