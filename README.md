# Garage 2.0 Group Project

## The model

### Design

* Id (int)
* Registration (string)
* VehicleType (VehicleType Enum) 
  * Car
  * Bus
  * MC
  * Carrier
* VehicleBrand (VehicleBrand Enum)
* Color (string)
* ParkingTime (DateTime)
* CheckOutTime (DateTime)
* Price (double)
* PricePerHour (double)
* IsParked (bool)

## The controller

### Design

* Index
* ParkVehicle
  * Vehicle.ParkingTime is set to DateTime.Now
* CheckOutVehicle
  * Vehicle.CheckOutTime is set to DateTime.Now and Vehicle.IsParked=false
  * Produce a receipt, calculate price, set IsParked to false
* List of all vehicles
* Filtering
  * IsParked = true or false
  * Search by Registration number  
  * Sort by all table attributes except Id

  ## The View

  ### Default View Locations

  * /Vehicles/ParkVehicle
  * /Vehicles/CheckOutVehicle
  * /Vehicles/ViewVehicles
 