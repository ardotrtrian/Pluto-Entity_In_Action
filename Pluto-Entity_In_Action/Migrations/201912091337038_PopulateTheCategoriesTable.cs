namespace Pluto_Entity_In_Action.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTheCategoriesTable : DbMigration
    {
        public override void Up()
        {
            Sql("insert into categories (name) values ('Web Development')");
            Sql("insert into categories (name) values ('Programming')");

        }

        public override void Down()
        {
        }
    }
}
