namespace Garage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "PricePerHour", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "PricePerHour");
        }
    }
}
