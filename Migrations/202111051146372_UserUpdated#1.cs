namespace course_proj_english_tutorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserUpdated1 : DbMigration
    {
        public override void Up()
        {
			AddColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 30));
			AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 50));
		}
        
        public override void Down()
        {
			AlterColumn("dbo.Users", "UserName", c => c.String());
			DropColumn("dbo.Users", "Password");
		}
    }
}
