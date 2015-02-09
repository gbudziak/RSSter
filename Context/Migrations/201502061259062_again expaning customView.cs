namespace RssDataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class againexpaningcustomView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserCustomViews", "ItemAge", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserCustomViews", "ItemAge");
        }
    }
}
