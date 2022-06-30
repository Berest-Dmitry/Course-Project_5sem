namespace course_proj_english_tutorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExerciseUpdated : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Exercises", "Answers", c => c.String());
        }
        
        public override void Down()
        {
            //DropColumn("dbo.Exercises", "Answers");
        }
    }
}
