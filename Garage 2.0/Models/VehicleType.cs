using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Garage.Models
{
    public class VehicleType
    {
        public int Id { get; set; }

        [DisplayName("Vehicle Type")]
        public string Type { get; set; }
    }
}