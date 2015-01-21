namespace DBContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jarekchanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Channels", "Url", c => c.String());
            AddColumn("dbo.Channels", "ImageUrl", c => c.String());
            AddColumn("dbo.Items", "Url", c => c.String());
            AddColumn("dbo.UserChannels", "IsHidden", c => c.Boolean(nullable: false));
            DropColumn("dbo.Channels", "Link");
            DropColumn("dbo.Channels", "Image");
            DropColumn("dbo.Items", "Link");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Link", c => c.String());
            AddColumn("dbo.Channels", "Image", c => c.String());
            AddColumn("dbo.Channels", "Link", c => c.String());
            DropColumn("dbo.UserChannels", "IsHidden");
            DropColumn("dbo.Items", "Url");
            DropColumn("dbo.Channels", "ImageUrl");
            DropColumn("dbo.Channels", "Url");
        }
    }
}
