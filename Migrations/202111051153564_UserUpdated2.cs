namespace course_proj_english_tutorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserUpdated2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "JournalId" });
            AlterColumn("dbo.Users", "JournalId", c => c.Guid());
            CreateIndex("dbo.Users", "JournalId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "JournalId" });
            AlterColumn("dbo.Users", "JournalId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Users", "JournalId");
        }
    }
}
