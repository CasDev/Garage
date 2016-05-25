using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public virtual ICollection<Vehicle> Vehicle { get; set; }
    }
}