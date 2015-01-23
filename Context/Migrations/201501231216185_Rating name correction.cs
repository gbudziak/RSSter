namespace DBContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ratingnamecorrection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "RatingPlus", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "RatingMinus", c => c.Int(nullable: false));
            AddColumn("dbo.UserItems", "RatingPlus", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserItems", "RatingMinus", c => c.Boolean(nullable: false));
            DropColumn("dbo.Items", "RaitingPlus");
            DropColumn("dbo.Items", "RaitingMinus");
            DropColumn("dbo.UserItems", "RaitingPlus");
            DropColumn("dbo.UserItems", "RaitingMinus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserItems", "RaitingMinus", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserItems", "RaitingPlus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Items", "RaitingMinus", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "RaitingPlus", c => c.Int(nullable: false));
            DropColumn("dbo.UserItems", "RatingMinus");
            DropColumn("dbo.UserItems", "RatingPlus");
            DropColumn("dbo.Items", "RatingMinus");
            DropColumn("dbo.Items", "RatingPlus");
        }
    }
}
