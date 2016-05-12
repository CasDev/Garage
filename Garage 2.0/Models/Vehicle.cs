using System;
using System.Collections.Generic;
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
        public string Registration { get; set; }
        public VehicleType VehicleType { get; set; }
        public VehicleBrand VehicleBrand { get; set; }
        public string Color { get; set; }
        public DateTime ParkingTime { get; set; }
        public DateTime CheckoutTime { get; set; }
        public double Price { get; set; }
        public bool IsParked { get; set; }
    }
}