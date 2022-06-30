namespace course_proj_english_tutorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VocabulariesUpdated1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vocabularies", "WordsTranslationsByteArray", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vocabularies", "WordsTranslationsByteArray");
        }
    }
}
