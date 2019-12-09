namespace Pluto_Entity_In_Action.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmodelversion3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authors", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Authors", "LastName");
        }
    }
}
