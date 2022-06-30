namespace course_proj_english_tutorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lessons_Updated1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "TimeStart", c => c.DateTime());
            AddColumn("dbo.Lessons", "TimeEnd", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lessons", "TimeEnd");
            DropColumn("dbo.Lessons", "TimeStart");
        }
    }
}
