namespace Garage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removed_ICollection_In_Vehicle_Model : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VehicleMembers", "Vehicle_Id", "dbo.Vehicles");
            DropForeignKey("dbo.VehicleMembers", "Member_Id", "dbo.Members");
            DropIndex("dbo.VehicleMembers", new[] { "Vehicle_Id" });
            DropIndex("dbo.VehicleMembers", new[] { "Member_Id" });
            AddColumn("dbo.Vehicles", "Member_Id", c => c.Int());
            CreateIndex("dbo.Vehicles", "Member_Id");
            AddForeignKey("dbo.Vehicles", "Member_Id", "dbo.Members", "Id");
            DropTable("dbo.VehicleMembers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VehicleMembers",
                c => new
                    {
                        Vehicle_Id = c.Int(nullable: false),
                        Member_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vehicle_Id, t.Member_Id });
            
            DropForeignKey("dbo.Vehicles", "Member_Id", "dbo.Members");
            DropIndex("dbo.Vehicles", new[] { "Member_Id" });
            DropColumn("dbo.Vehicles", "Member_Id");
            CreateIndex("dbo.VehicleMembers", "Member_Id");
            CreateIndex("dbo.VehicleMembers", "Vehicle_Id");
            AddForeignKey("dbo.VehicleMembers", "Member_Id", "dbo.Members", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VehicleMembers", "Vehicle_Id", "dbo.Vehicles", "Id", cascadeDelete: true);
        }
    }
}
