namespace Garage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ninth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        ZipCode = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParkedVehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        ParkingTime = c.DateTime(nullable: false),
                        CheckoutTime = c.DateTime(nullable: false),
                        PricePerHour = c.Double(nullable: false),
                        IsParked = c.Boolean(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.VehicleId);
            
            DropColumn("dbo.Vehicles", "VehicleBrand");
            DropColumn("dbo.Vehicles", "ParkingTime");
            DropColumn("dbo.Vehicles", "CheckoutTime");
            DropColumn("dbo.Vehicles", "TotalPrice");
            DropColumn("dbo.Vehicles", "PricePerHour");
            DropColumn("dbo.Vehicles", "IsParked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "IsParked", c => c.Boolean(nullable: false));
            AddColumn("dbo.Vehicles", "PricePerHour", c => c.Double(nullable: false));
            AddColumn("dbo.Vehicles", "TotalPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Vehicles", "CheckoutTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Vehicles", "ParkingTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Vehicles", "VehicleBrand", c => c.Int(nullable: false));
            DropForeignKey("dbo.ParkedVehicles", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.ParkedVehicles", "MemberId", "dbo.Members");
            DropIndex("dbo.ParkedVehicles", new[] { "VehicleId" });
            DropIndex("dbo.ParkedVehicles", new[] { "MemberId" });
            DropTable("dbo.ParkedVehicles");
            DropTable("dbo.Members");
        }
    }
}
