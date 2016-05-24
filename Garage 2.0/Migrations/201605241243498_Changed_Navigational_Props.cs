namespace Garage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_Navigational_Props : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ParkedVehicles", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.ParkedVehicles", "VehicleId", "dbo.Vehicles");
            DropIndex("dbo.ParkedVehicles", new[] { "MemberId" });
            DropIndex("dbo.ParkedVehicles", new[] { "VehicleId" });
            DropIndex("dbo.Vehicles", new[] { "VehicleTypeId" });
            CreateTable(
                "dbo.VehicleMembers",
                c => new
                    {
                        Vehicle_Id = c.Int(nullable: false),
                        Member_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vehicle_Id, t.Member_Id })
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_Id, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.Member_Id, cascadeDelete: true)
                .Index(t => t.Vehicle_Id)
                .Index(t => t.Member_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleMembers", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.VehicleMembers", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.VehicleMembers", new[] { "Member_Id" });
            DropIndex("dbo.VehicleMembers", new[] { "Vehicle_Id" });
            DropTable("dbo.VehicleMembers");
            CreateIndex("dbo.Vehicles", "VehicleTypeId");
            CreateIndex("dbo.ParkedVehicles", "VehicleId");
            CreateIndex("dbo.ParkedVehicles", "MemberId");
            AddForeignKey("dbo.ParkedVehicles", "VehicleId", "dbo.Vehicles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ParkedVehicles", "MemberId", "dbo.Members", "Id", cascadeDelete: true);
        }
    }
}
