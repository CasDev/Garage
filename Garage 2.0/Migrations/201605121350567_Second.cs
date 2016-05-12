namespace Garage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "TotalPrice", c => c.Double(nullable: false));
            DropColumn("dbo.Vehicles", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.Vehicles", "TotalPrice");
        }
    }
}
