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
        OTHER, BUS, CAR, CARRIER, MC
    }

    public enum VehicleBrand
    {
        OTHER, AUDI, BMW, MERCEDES
    }

    public class Vehicle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You need to specify a registration number")]
        [MaxLength(101, ErrorMessage = "Cannot be longer than 100 characters")]
        public string Registration { get; set; }

        [DisplayName("Vehicle Type")]
        public VehicleType VehicleType { get; set; }

        [DisplayName("Vehicle Brand")]
        public VehicleBrand VehicleBrand { get; set; }

        [Required(ErrorMessage = "You need to specify a color")]
        [MaxLength(101, ErrorMessage = "Cannot be longer then 100 characters")]
        public string Color { get; set; }

        [DisplayName("Parked Since")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime ParkingTime { get; set; }

        [DisplayName("Parked To")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime CheckoutTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:#####.##}")]
        [DisplayName("Total Price")]
        public double TotalPrice { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:#####.##}")]
        [DisplayName("Price Per Hour")]
        public double PricePerHour { get; set; }

        [DisplayName("Is Parked?")]
        public bool IsParked { get; set; }        
    }
}