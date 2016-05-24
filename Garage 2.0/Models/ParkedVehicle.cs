using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage.Models
{
    public class ParkedVehicle
    {
        public int MemberId { get; set; }
        public int VehicleId { get; set; }

        [DisplayName("Parked Since")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime ParkingTime { get; set; }

        public virtual Vehicle Vehicle { get; set; }
        public virtual Member Member { get; set; }

        [DisplayName("Parked To")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime CheckoutTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:#####.##}")]
        [DisplayName("Price Per Hour")]
        public double PricePerHour { get; set; }

        [DisplayName("Is Parked?")]
        public bool IsParked { get; set; }

        [DisplayFormat(DataFormatString = "{0:#####.##}")]
        [DisplayName("Total Price")]
        public double TotalPrice { get; set; }
    }
}