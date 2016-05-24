namespace Garage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Eighth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VehicleTypes", "Type", c => c.String());
            DropColumn("dbo.VehicleTypes", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VehicleTypes", "Title", c => c.String());
            DropColumn("dbo.VehicleTypes", "Type");
        }
    }
}
