namespace Garage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "PricePerHour", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "PricePerHour", c => c.Double());
        }
    }
}
