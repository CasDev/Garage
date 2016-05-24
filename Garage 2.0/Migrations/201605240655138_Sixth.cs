namespace Garage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sixth : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER DATABASE Garage COLLATE Finnish_Swedish_CI_AS;", suppressTransaction: true);

            AlterColumn("dbo.Vehicles", "Registration", c => c.String(nullable: false, maxLength: 101));
            AlterColumn("dbo.Vehicles", "Color", c => c.String(nullable: false, maxLength: 101));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "Color", c => c.String(nullable: false));
            AlterColumn("dbo.Vehicles", "Registration", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
