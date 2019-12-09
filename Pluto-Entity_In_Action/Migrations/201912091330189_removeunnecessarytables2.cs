namespace Pluto_Entity_In_Action.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeunnecessarytables2 : DbMigration
    {
        public override void Up()
        {
            DropTable("tagclasses");
            DropTable("classes");
            DropTable("instructors");
        }
        
        public override void Down()
        {
        }
    }
}
