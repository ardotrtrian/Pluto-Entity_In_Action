﻿namespace Pluto_Entity_In_Action.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeunnecessarytables : DbMigration
    {
        public override void Up()
        {
            DropTable("tagss");
        }
        
        public override void Down()
        {
        }
    }
}
