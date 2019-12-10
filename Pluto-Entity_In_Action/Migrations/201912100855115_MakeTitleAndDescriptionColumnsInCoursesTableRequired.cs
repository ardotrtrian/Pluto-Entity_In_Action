namespace Pluto_Entity_In_Action.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeTitleAndDescriptionColumnsInCoursesTableRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Description", c => c.String());
            AlterColumn("dbo.Courses", "Title", c => c.String());
        }
    }
}
