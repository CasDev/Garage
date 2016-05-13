using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage.Models
{
    public enum VehicleType
    {
        OTHER, CAR, BUS, MC, CARRIER
    }

    public enum VehicleBrand
    {
        OTHER, AUDI, BMW, MERCEDES
    }

    public class Vehicle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You need to add registration number")]
        public string Registration { get; set; }

        [DisplayName("Vehicle Type")]
        public VehicleType VehicleType { get; set; }

        [DisplayName("Vehicle Brand")]
        public VehicleBrand VehicleBrand { get; set; }

        [Required(ErrorMessage = "You need to add a color")]
        public string Color { get; set; }

        [DisplayName("Parked Since")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime ParkingTime { get; set; }

        [DisplayName("Parked To")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? CheckoutTime { get; set; }

        [DisplayName("Total Price")]
        public double TotalPrice { get; set; }

        [DisplayName("Price Per Hour")]
        public double PricePerHour { get; set; }

        [DisplayName("Is Parked?")]
        public bool IsParked { get; set; }        
    }
}