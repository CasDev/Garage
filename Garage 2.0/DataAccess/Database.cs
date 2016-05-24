using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Garage.DataAccess
{
    public class Database : DbContext
    {
        //        public Database() : base("DefaultConnection") { }
        public Database() : base("DefaultConnection2") { }

        public DbSet<Models.Vehicle> Vehicles { get; set; }

        public DbSet<Models.VehicleType> VehicleTypes { get; set; }

        public DbSet<Models.Member> Members { get; set; }

        public DbSet<Models.ParkedVehicle> ParkedVehicles { get; set; }
    }
}