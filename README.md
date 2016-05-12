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
* IsParked (bool) // IsParked { get { return (this.CheckOutTime != null) } } might be a future idea

## The controller

### Design

* Index
* List of all vehicles
  * Filter by Vehicle.IsParked to find current, and historical, parkings
* Park
  * Vehicle.ParkingTime is set to DateTime.Now
  * Vehicle.Price is set to current price
* CheckOut
  * Vehicle.CheckOutTime is set to DateTime.Now and Vehicle.IsParked=false
  * Produce a receipt
  * calculate price ( Vehicle.PricePerHour = Vehicle.Price, Vehicle.Price = ( Vehicle.Price * amountOfHours ) )
  * set IsParked to false
* Filtering
  * Search by Registration number  
  * Sort by all table attributes except Id

  ## The View

  ### Default View Locations

  * /Vehicles/ParkVehicle
  * /Vehicles/CheckOutVehicle
  * /Vehicles/ViewVehicles
 