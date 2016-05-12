using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Garage.DataAccess
{
    public class Database : DbContext
    {
        public Database() : base("DefaultConnection") { }

        public DbSet<Models.Vehicle> Vehicles { get; set; }
    }
}