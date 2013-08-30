namespace TimeTrackr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renametotask : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeInterval", "IntervalOwner_UserId", "dbo.UserProfile");
            DropIndex("dbo.TimeInterval", new[] { "IntervalOwner_UserId" });
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        Category = c.String(),
                        TaskOwner_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserProfile", t => t.TaskOwner_UserId)
                .Index(t => t.TaskOwner_UserId);
            
            DropTable("dbo.TimeInterval");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TimeInterval",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        Category = c.String(),
                        IntervalOwner_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropIndex("dbo.Task", new[] { "TaskOwner_UserId" });
            DropForeignKey("dbo.Task", "TaskOwner_UserId", "dbo.UserProfile");
            DropTable("dbo.Task");
            CreateIndex("dbo.TimeInterval", "IntervalOwner_UserId");
            AddForeignKey("dbo.TimeInterval", "IntervalOwner_UserId", "dbo.UserProfile", "UserId");
        }
    }
}
