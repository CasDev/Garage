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

namespace Garage.Controllers
{
    public class Vehicles2Controller : Controller
    {
        private DataAccess.Database db = new DataAccess.Database();

        // GET: Vehicles2
        public ActionResult Index()
        {
            var vehicles = db.Vehicles.Include(m => m.Member);
            return View(vehicles.ToList());
        }

        // GET: Vehicles2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles2/Create
        public ActionResult Create()
        {
            ViewBag.VehicleTypes = db.VehicleTypes.OrderBy(t => t.Type);
            if (((IEnumerable<VehicleType>)ViewBag.VehicleTypes).Count() <= 0)
            {
                // TODO: warning
            }
            ViewBag.Members = db.Members.OrderBy(m => m.LastName);
            if (((IEnumerable<Member>) ViewBag.Members).Count() <= 0)
            {
                // TODO: warning
            }

            return View();
        }

        // POST: Vehicles2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Registration,VehicleTypeId,MemberTypeId,Color")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                Member Member = db.Members.Find(vehicle.MemberTypeId);
                vehicle.Member = Member;
                vehicle.Registration.ToUpper();
                vehicle.Color.ToUpper();
                db.Vehicles.Add(vehicle);
                Member.Vehicle.Add(vehicle);
//                db.Members.Add(Member);
// TODO: this is throwing an exception, and I don't know why
                db.SaveChanges();

                ParkedVehicle parked = new ParkedVehicle();
                parked.CheckoutTime = new DateTime(1970, 1, 1, 0, 0, 0);
                parked.IsParked = true;
                parked.MemberId = Member.Id;
                parked.ParkingTime = DateTime.Now;
                parked.PricePerHour = 60;
                // TODO: set to configurated price
                parked.TotalPrice = 0;
                parked.VehicleId = vehicle.Id;
                db.ParkedVehicles.Add(parked);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        // GET: Vehicles2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }

            var list = db.VehicleTypes.OrderBy(t => t.Type);
            if (list.Count() <= 0)
            {
                // TODO: warning
            }
            var _list = new List<SelectListItem>();
            foreach (var t in list)
            {
                _list.Add(new SelectListItem() { Text = t.Type, Value = t.Id.ToString() });
            }
            ViewBag.VehicleTypes = _list;

            return View(vehicle);
        }

        // POST: Vehicles2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Registration,VehicleTypeId,Color")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.Color.ToUpper();
                vehicle.Registration.ToUpper();
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: Vehicles2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return RedirectToAction("Index");
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
