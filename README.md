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
* TotalPrice (double)
* PricePerHour (double)
* IsParked (bool)

## The controller

### Design

* Index
* List of all vehicles
  * Filter by Vehicle.IsParked to find current, and historical, parkings
* Park
  * Vehicle.ParkingTime is set to DateTime.Now
  * Vehicle.TotalPrice is set to current price, or 0 ( zero )
  * Vehicle.PricePerHour is set to current price
* CheckOut
  * Vehicle.CheckOutTime is set to DateTime.Now and Vehicle.IsParked=false
  * Produce a receipt
  * calculate price ( Vehicle.PricePerHour = Vehicle.TotalPrice, Vehicle.TotalPrice = ( Vehicle.TotalPrice * amountOfHours ) )
  * set IsParked to false
* CheckOut By Search
  * search by Vehicle.Registration
* Filtering
  * Search by Registration number  
  * Sort by all table attributes except Id

## The View

### Default View Locations

  * /Vehicles/ParkVehicle
  * /Vehicles/CheckOutVehicle
  * /Vehicles/ViewVehicles
 

 ### Sorting, filtering and paging

 Support for sorting, filtering and paging has been added to the index view of the vehicles.