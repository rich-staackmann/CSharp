namespace TimeTrackr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Categories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeInterval", "Category", c => c.String());
            AlterColumn("dbo.TimeInterval", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeInterval", "Description", c => c.String());
            DropColumn("dbo.TimeInterval", "Category");
        }
    }
}
