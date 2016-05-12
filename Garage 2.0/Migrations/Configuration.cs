namespace Garage.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage.DataAccess.Database>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Garage.DataAccess.Database context)
        {
            //  This method will be called after migrating to the latest version.

            context.Vehicles.AddOrUpdate(v => new { v.Registration, v.ParkingTime },
                new Vehicle() { Registration = "ABC123", VehicleType = VehicleType.CAR, VehicleBrand = VehicleBrand.OTHER, Color = "RED", ParkingTime = new DateTime(2016, 5, 1, 17, 0, 0), CheckoutTime = new DateTime(2016, 5, 1, 22, 0, 0), TotalPrice = 61, PricePerHour = 61, IsParked = false },
                new Vehicle() { Registration = "CAB123", VehicleType = VehicleType.MC, VehicleBrand = VehicleBrand.OTHER, Color = "RED", ParkingTime = new DateTime(2016, 5, 11, 17, 0, 0), CheckoutTime = null, TotalPrice = 61, IsParked = true },
                new Vehicle() { Registration = "ACB123", VehicleType = VehicleType.OTHER, VehicleBrand = VehicleBrand.OTHER, Color = "RED", ParkingTime = new DateTime(2016, 5, 10, 17, 0, 0), CheckoutTime = null, TotalPrice = 61, IsParked = true },
                new Vehicle() { Registration = "CBA123", VehicleType = VehicleType.CAR, VehicleBrand = VehicleBrand.OTHER, Color = "RED", ParkingTime = DateTime.Now, CheckoutTime = null, TotalPrice = 61, IsParked = true }
            );
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
