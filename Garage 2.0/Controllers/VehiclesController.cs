using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage.DataAccess;
using Garage.Models;
using System.Configuration;
using System.Globalization;
using PagedList;

namespace Garage.Controllers
{
    public class VehiclesController : Controller
    {
        private DataAccess.Database db = new DataAccess.Database();

        // GET: Vehicles
        [ValidateInput(false)]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string Message)
        {
            ViewBag.Message = (Message != null ? HttpUtility.UrlDecode(Message) : null);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.RegistrationParm = String.IsNullOrEmpty(sortOrder) ? "registration_desc" : "";
            ViewBag.ParkingTimeParm = sortOrder == "ParkingTime" ? "parkingtime_desc" : "ParkingTime";
            ViewBag.ColorParm = sortOrder == "Color" ? "color_desc" : "Color";
            ViewBag.VehicleTypeParm = sortOrder == "VehicleType" ? "vehicletype_desc" : "VehicleType";
            ViewBag.VehicleBrandParm = sortOrder == "VehicleBrand" ? "vehiclebrand_desc" : "VehicleBrand";

            var vehicles = new List<Vehicle>();
            foreach (var parked in db.ParkedVehicles.Where(p => p.IsParked == true)) {
               vehicles.Add(db.Vehicles.Find(parked.VehicleId));
            }

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(v => v.Registration.Contains(searchString) || v.Color.Contains(searchString)).ToList();
                // TODO: must check for type
            }

            switch (sortOrder)
            {
                case "registration_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Registration).ToList();
                    break;
                case "vehicletype_desc":
//                    vehicles = vehicles.OrderByDescending(v => v.vehicletype).ToList();
// TODO: fix this
                    break;
                case "vehicletype":
//                    vehicles = vehicles.OrderBy(v => v.vehicletype).ToList();
// TODO: fix this
                    break;
                case "parkingtime_desc":
//                    vehicles = vehicles.OrderByDescending(v => v.parkingtime).ToList();
                    break;
                case "parkingtime":
//                    vehicles = vehicles.OrderBy(v => v.parkingtime).ToList();
                    break;
                case "color_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Color).ToList();
                    break;
                case "color":
                    vehicles = vehicles.OrderBy(v => v.Color).ToList();
                    break;
                default:
                    vehicles = vehicles.OrderBy(v => v.Registration).ToList();
                    break;
            }

            string DefaultPageSize = ConfigurationManager.AppSettings["PageSize"];
            
            int pageSize = 1;
            Int32.TryParse(DefaultPageSize, out pageSize);
            int pageNumber = (page ?? 1);
            return View(vehicles.ToPagedList(pageNumber, pageSize));
            
        }

        public ActionResult Historic()
        {
            var vehicles = new List<Vehicle>();
            foreach (var parked in db.ParkedVehicles.Where(p => p.IsParked == false))
            {
                vehicles.Add(db.Vehicles.Find(parked.VehicleId));
            }

            //return View(db.Vehicles.Where(v => v.IsParked == false).ToList());
            return View(vehicles);
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return RedirectToAction("Index", new { Message = Url.Encode("Vehicle not found") });
            }
            return View(vehicle);
        }

        // GET: Vehicles/Park
        public ActionResult Park()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Park([Bind(Include = "Registration,VehicleType,VehicleBrand,Color")] Vehicle vehicle)
        {
            // TODO: take a look at this function
            if (ModelState.IsValid)
            {
                // check if a vehicle with that registration number
                //if (db.Vehicles.Where(v => v.IsParked == true && v.Registration.ToUpper() == vehicle.Registration.ToUpper()).Any())
                //    return RedirectToAction("Index", new { Message = Url.Encode("A vehicle with that registration number is already parked") } );
                
                double DefaultMoney = 0;
                string _DefaultPricePerHour = ConfigurationManager.AppSettings["DefaultPricePerHour"];
                if (_DefaultPricePerHour == null)
                {
                    _DefaultPricePerHour = "0";
                }
                _DefaultPricePerHour = _DefaultPricePerHour.Replace('.', ',').Trim().Replace(" ", "");

                NumberStyles style = NumberStyles.AllowDecimalPoint;
                CultureInfo culture = CultureInfo.CreateSpecificCulture("se-SV");
                Double.TryParse(_DefaultPricePerHour, style, culture, out DefaultMoney);

                //vehicle.IsParked = true;
                //vehicle.ParkingTime = DateTime.Now;
                //vehicle.CheckoutTime = new DateTime(1970, 1, 1);
                //vehicle.TotalPrice = DefaultMoney;
                //vehicle.PricePerHour = DefaultMoney;
                vehicle.Color = vehicle.Color.ToUpper();
                vehicle.Registration = vehicle.Registration.ToUpper();

                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                

                             
                return RedirectToAction("Details", new { Id = vehicle.Id });
            }

            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return RedirectToAction("Index", new { Message = Url.Encode("Vehicle not found") });
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Registration,VehicleType,VehicleBrand,Color")] Vehicle vehicle)
        {

            //if (db.Vehicles.Where(v => v.IsParked == true && v.Registration.ToUpper() == vehicle.Registration.ToUpper()).Any())
            //    return RedirectToAction("Index", new { Message = Url.Encode("A vehicle with that registration number is already parked") });

            // TODO: check up on this one
            if (ModelState.IsValid)
            {
                Vehicle _vehicle = db.Vehicles.Find(vehicle.Id);
                if (_vehicle == null)
                {
                    return RedirectToAction("Index", new { Message = Url.Encode("Vehicle not found") });
                }
                _vehicle.Registration = vehicle.Registration.ToUpper();
                //_vehicle.VehicleBrand = vehicle.VehicleBrand;
                //_vehicle.VehicleType = vehicle.VehicleType;
                _vehicle.Color = vehicle.Color.ToUpper();

                db.Entry(_vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: Vehicles/CheckOut/5
        public ActionResult CheckOut(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Vehicles/CheckOut_alt.cshtml");
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return RedirectToAction("Index", new { Message = Url.Encode("Vehicle not found") });
            }
            return View(vehicle);
        }

        [HttpGet, ActionName("CheckOutSearch")]
        public ActionResult Search()
        {
            return View("~/Views/Vehicles/CheckOut_alt.cshtml");
        }

        [HttpPost, ActionName("CheckOutSearch")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSearch(FormCollection Collection)
        {
            string Search = Collection["Registration"];
            if (string.IsNullOrEmpty(Search) || string.IsNullOrWhiteSpace(Search))
            {
                ViewBag.Wrong = "Please add a registration number";
                return View("~/Views/Vehicles/CheckOut_alt.cshtml");
            }

            Vehicle vehicle = db.Vehicles.FirstOrDefault(v => v.Registration.Equals(Search));
            if (vehicle == null)
            {
                return RedirectToAction("Index", new { Message = Url.Encode("Vehicle not found"), searchString = Search });
            }
            return View("~/Views/Vehicles/CheckOut.cshtml", vehicle);
        }

        // POST: Vehicles/CheckOut/5
        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParkedVehicle parked = db.ParkedVehicles.Where(p => p.VehicleId == id && p.IsParked == true).FirstOrDefault();
            if (parked == null)
            {
                return RedirectToAction("Index", new { Message = Url.Encode("Parked vehicle not found") });
            }
            parked.CheckoutTime = DateTime.Now;
            parked.IsParked = false;
            
            db.Entry(parked).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Receipt", new { id = parked.Id });
        }

        public ActionResult Receipt(int id)
        {
            ParkedVehicle parked = db.ParkedVehicles.Find(id);
            if (parked == null)
            {
                return RedirectToAction("Index", new { Message = Url.Encode("Parked vehicle not found") });
            }
            TimeSpan duration = (DateTime.Now - parked.ParkingTime);
            double totalPrice = duration.TotalMinutes * parked.PricePerHour / 60.0;

            parked.TotalPrice = totalPrice;
            db.Entry(parked).State = EntityState.Modified;
            db.SaveChanges();

            return View(parked);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
