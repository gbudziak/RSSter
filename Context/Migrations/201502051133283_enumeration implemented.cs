namespace RssDataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enumerationimplemented : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserCustomViews", "ViewType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserCustomViews", "ViewType", c => c.Short(nullable: false));
        }
    }
}
