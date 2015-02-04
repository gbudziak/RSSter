namespace RssDataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedviewNametocustomViewModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserCustomViews", "ViewType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserCustomViews", "ViewType");
        }
    }
}
