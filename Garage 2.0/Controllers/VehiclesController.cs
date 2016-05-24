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

            var vehicles = db.Vehicles.Where(v => v.IsParked == true);

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
                vehicles = vehicles.Where(v => v.Registration.Contains(searchString) ||
                                          v.Color.Contains(searchString) ||
                                          searchString.Contains(v.VehicleType.ToString()) ||
                                          searchString.Contains(v.VehicleBrand.ToString()));
            }

            switch (sortOrder)
            {
                case "registration_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Registration);
                    break;
                case "vehicletype_desc":
                    vehicles = vehicles.OrderByDescending(v => v.VehicleType);
                    break;
                case "VehicleType":
                    vehicles = vehicles.OrderBy(v => v.VehicleType);
                    break;
                case "vehiclebrand_desc":
                    vehicles = vehicles.OrderByDescending(v => v.VehicleBrand);
                    break;
                case "VehicleBrand":
                    vehicles = vehicles.OrderBy(v => v.VehicleBrand);
                    break;
                case "parkingtime_desc":
                    vehicles = vehicles.OrderByDescending(v => v.ParkingTime);
                    break;
                case "ParkingTime":
                    vehicles = vehicles.OrderBy(v => v.ParkingTime);
                    break;
                case "color_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Color);
                    break;
                case "Color":
                    vehicles = vehicles.OrderBy(v => v.Color);
                    break;
                default:
                    vehicles = vehicles.OrderBy(v => v.Registration);
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
            return View(db.Vehicles.Where(v => v.IsParked == false).ToList());
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
            if (ModelState.IsValid)
            {
                string regNumber = vehicle.Registration.ToUpper();

                var result = db.Vehicles.Where(v => v.Registration == regNumber).ToList();

                if (result.Count > 0)
                {
                    return RedirectToAction("Index");
                }
                
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

                vehicle.IsParked = true;
                vehicle.ParkingTime = DateTime.Now;
                vehicle.CheckoutTime = new DateTime(1970, 1, 1);
                vehicle.TotalPrice = DefaultMoney;
                vehicle.PricePerHour = DefaultMoney;
                vehicle.Color = vehicle.Color.ToUpper();
                vehicle.Registration = vehicle.Registration.ToUpper();

                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
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
            if (ModelState.IsValid)
            {
                Vehicle _vehicle = db.Vehicles.Find(vehicle.Id);
                if (_vehicle == null)
                {
                    return RedirectToAction("Index", new { Message = Url.Encode("Vehicle not found") });
                }
                _vehicle.Registration = vehicle.Registration.ToUpper();
                _vehicle.VehicleBrand = vehicle.VehicleBrand;
                _vehicle.VehicleType = vehicle.VehicleType;
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
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return RedirectToAction("Index", new { Message = Url.Encode("Vehicle not found") });
            }
            vehicle.CheckoutTime = DateTime.Now;
            
            vehicle.IsParked = false;
            db.Entry(vehicle).State = EntityState.Modified;
            db.SaveChanges();
            //db.Vehicles.Remove(vehicle);
            //db.SaveChanges();
            return RedirectToAction("Receipt", new { id = vehicle.Id });
        }

        public ActionResult Receipt(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return RedirectToAction("Index", new { Message = Url.Encode("Vehicle not found") });
            }
            TimeSpan duration = (DateTime.Now - vehicle.ParkingTime);
            double totalPrice = duration.TotalMinutes * vehicle.PricePerHour / 60.0;

            vehicle.TotalPrice = totalPrice;
            db.Entry(vehicle).State = EntityState.Modified;
            db.SaveChanges();

            return View(vehicle);
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
