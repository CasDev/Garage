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
            VehicleType[] Types = new[]
            {
                new VehicleType() { Type = "CAR" },
                new VehicleType() { Type = "MC" },
                new VehicleType() { Type = "BUS" },
                new VehicleType() { Type = "OTHER" }
            };
            context.VehicleTypes.AddOrUpdate(v => v.Type,
                Types
            );
            context.SaveChanges();

            Vehicle[] Vehicles = new[]
            {
                new Vehicle() { Registration = "ABC123", VehicleTypeId = Types[0].Id, Color = "RED" },
                new Vehicle() { Registration = "CAB123", VehicleTypeId = Types[1].Id, Color = "RED" },
                new Vehicle() { Registration = "ACB123", VehicleTypeId = Types[3].Id, Color = "RED" },
                new Vehicle() { Registration = "CBA123", VehicleTypeId = Types[0].Id, Color = "RED" }
            };

            context.Vehicles.AddOrUpdate(v => v.Registration,
                Vehicles
            );
            context.SaveChanges();

            Member[] Members = new[]
            {
                new Member() { FirstName = "John", LastName = "Castell", Address = "Storgatan 2", City = "Tyresö", ZipCode = "172 92", RegistrationDate = DateTime.Now, IsActive = true },
                new Member() { FirstName = "Andreas", LastName = "Carsbring", Address = "Storgatan 4", City = "Tyresö", ZipCode = "172 92", RegistrationDate = DateTime.Now, IsActive = true },
                new Member() { FirstName = "Thomas", LastName = "Ekman", Address = "Storgatan 1", City = "Tyresö", ZipCode = "172 92", RegistrationDate = DateTime.Now, IsActive = true }
            };
            context.Members.AddOrUpdate(m => new { m.FirstName, m.LastName },
                Members
            );
            context.SaveChanges();

            ParkedVehicle[] Parked = new[]
            {
                new ParkedVehicle() { ParkingTime = new DateTime(2016, 5, 18), CheckoutTime = new DateTime(2016, 5, 1, 22, 0, 0), TotalPrice = 61, PricePerHour = 61, IsParked = false, MemberId = Members[0].Id, VehicleId = Vehicles[0].Id },
                new ParkedVehicle() { ParkingTime = new DateTime(2016, 5, 20), CheckoutTime = DateTime.Now, TotalPrice = 0, PricePerHour = 61, IsParked = true, MemberId = Members[2].Id, VehicleId = Vehicles[1].Id },
                new ParkedVehicle() { ParkingTime = new DateTime(2016, 5, 22), CheckoutTime = DateTime.Now, TotalPrice = 0, PricePerHour = 61, IsParked = true, MemberId = Members[2].Id, VehicleId = Vehicles[2].Id },
                new ParkedVehicle() { ParkingTime = new DateTime(2016, 5, 23), CheckoutTime = DateTime.Now, TotalPrice = 0, PricePerHour = 61, IsParked = true, MemberId = Members[1].Id, VehicleId = Vehicles[3].Id }
            };
            context.ParkedVehicles.AddOrUpdate(
                p => new { p.ParkingTime, p.MemberId, p.VehicleId, p.IsParked },
                Parked
            );
            context.SaveChanges();


            var membersList = context.Members.Include(m => m.Vehicle).ToArray();
            membersList[0].Vehicle.Add(Vehicles[0]);
            membersList[0].Vehicle.Add(Vehicles[2]);

            membersList[1].Vehicle.Add(Vehicles[1]);
            membersList[1].Vehicle.Add(Vehicles[3]);

            context.SaveChanges();
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
