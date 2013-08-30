namespace TimeTrackr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedkeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Task", "TaskOwner_UserId", "dbo.UserProfile");
            DropIndex("dbo.Task", new[] { "TaskOwner_UserId" });
            AddColumn("dbo.Task", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Task", "Category", c => c.String(nullable: false));
            AddForeignKey("dbo.Task", "UserID", "dbo.UserProfile", "UserId", cascadeDelete: true);
            CreateIndex("dbo.Task", "UserID");
            DropColumn("dbo.Task", "TaskOwner_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Task", "TaskOwner_UserId", c => c.Int());
            DropIndex("dbo.Task", new[] { "UserID" });
            DropForeignKey("dbo.Task", "UserID", "dbo.UserProfile");
            AlterColumn("dbo.Task", "Category", c => c.String());
            DropColumn("dbo.Task", "UserID");
            CreateIndex("dbo.Task", "TaskOwner_UserId");
            AddForeignKey("dbo.Task", "TaskOwner_UserId", "dbo.UserProfile", "UserId");
        }
    }
}
