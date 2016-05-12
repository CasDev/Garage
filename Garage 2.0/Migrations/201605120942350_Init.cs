namespace Garage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Registration = c.String(),
                        VehicleType = c.Int(nullable: false),
                        VehicleBrand = c.Int(nullable: false),
                        Color = c.String(),
                        ParkingTime = c.DateTime(nullable: false),
                        CheckoutTime = c.DateTime(),
                        Price = c.Double(nullable: false),
                        IsParked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vehicles");
        }
    }
}
