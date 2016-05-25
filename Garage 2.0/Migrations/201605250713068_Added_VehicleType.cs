namespace Garage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_VehicleType : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Vehicles", "VehicleTypeId");
            AddForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes");
            DropIndex("dbo.Vehicles", new[] { "VehicleTypeId" });
        }
    }
}
