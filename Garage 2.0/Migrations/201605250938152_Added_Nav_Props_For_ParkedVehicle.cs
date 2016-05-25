namespace Garage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Nav_Props_For_ParkedVehicle : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ParkedVehicles", "MemberId");
            CreateIndex("dbo.ParkedVehicles", "VehicleId");
            AddForeignKey("dbo.ParkedVehicles", "MemberId", "dbo.Members", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ParkedVehicles", "VehicleId", "dbo.Vehicles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkedVehicles", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.ParkedVehicles", "MemberId", "dbo.Members");
            DropIndex("dbo.ParkedVehicles", new[] { "VehicleId" });
            DropIndex("dbo.ParkedVehicles", new[] { "MemberId" });
        }
    }
}
