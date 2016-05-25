namespace Garage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tenth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.ParkedVehicles", "MemberId", "dbo.Members");
            DropForeignKey("dbo.ParkedVehicles", "VehicleId", "dbo.Vehicles");
            DropIndex("dbo.Vehicles", new[] { "VehicleTypeId" });
            DropIndex("dbo.ParkedVehicles", new[] { "MemberId" });
            DropIndex("dbo.ParkedVehicles", new[] { "VehicleId" });
            AddColumn("dbo.Members", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Members", "RegistrationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Vehicles", "VehicleTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.ParkedVehicles", "MemberId", c => c.Int(nullable: false));
            AlterColumn("dbo.ParkedVehicles", "VehicleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Vehicles", "VehicleTypeId");
            CreateIndex("dbo.ParkedVehicles", "MemberId");
            CreateIndex("dbo.ParkedVehicles", "VehicleId");
            AddForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ParkedVehicles", "MemberId", "dbo.Members", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ParkedVehicles", "VehicleId", "dbo.Vehicles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkedVehicles", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.ParkedVehicles", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes");
            DropIndex("dbo.ParkedVehicles", new[] { "VehicleId" });
            DropIndex("dbo.ParkedVehicles", new[] { "MemberId" });
            DropIndex("dbo.Vehicles", new[] { "VehicleTypeId" });
            AlterColumn("dbo.ParkedVehicles", "VehicleId", c => c.Int());
            AlterColumn("dbo.ParkedVehicles", "MemberId", c => c.Int());
            AlterColumn("dbo.Vehicles", "VehicleTypeId", c => c.Int());
            DropColumn("dbo.Members", "RegistrationDate");
            DropColumn("dbo.Members", "IsActive");
            CreateIndex("dbo.ParkedVehicles", "VehicleId");
            CreateIndex("dbo.ParkedVehicles", "MemberId");
            CreateIndex("dbo.Vehicles", "VehicleTypeId");
            AddForeignKey("dbo.ParkedVehicles", "VehicleId", "dbo.Vehicles", "Id");
            AddForeignKey("dbo.ParkedVehicles", "MemberId", "dbo.Members", "Id");
            AddForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes", "Id");
        }
    }
}
