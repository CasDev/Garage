﻿using System;
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
    public class VehiclesController : Controller
    {
        private DataAccess.Database db = new DataAccess.Database();

        // GET: Vehicles
        public ActionResult Index(string Message)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                ViewBag.Message = HttpUtility.UrlDecode(Message);
            }
            return View(db.Vehicles.Where(v => v.IsParked == true).ToList());
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
                return RedirectToAction("Index", new { Message = Url.Encode("Veichle not found") });
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
                vehicle.IsParked = true;
                vehicle.ParkingTime = DateTime.Now;
                vehicle.TotalPrice = 60; // TODO: add from config
                vehicle.PricePerHour = 60; // TODO: also from config
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
                return RedirectToAction("Index", new { Message = Url.Encode("Veichle not found") });
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VehicleType,VehicleBrand,Color")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
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
                return RedirectToAction("Index", new { Message = Url.Encode("Veichle not found") });
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
                ViewBag.Wrong = "Please, add a registration number";
                return View("~/Views/Vehicles/CheckOut_alt.cshtml");
            }

            Vehicle vehicle = db.Vehicles.FirstOrDefault(v => v.Registration.Equals(Search));
            if (vehicle == null)
            {
                return RedirectToAction("Index", new { Message = Url.Encode("Veichle not found") });
            }
            return View("~/Views/Vehicles/CheckOut.cshtml", vehicle);
        }

        // POST: Vehicles/CheckOut/5
        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id, FormCollection Collection)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            vehicle.CheckoutTime = DateTime.Now;
            
            vehicle.IsParked = false;
            db.Entry(vehicle).State = EntityState.Modified;
            db.SaveChanges();
            //db.Vehicles.Remove(vehicle);
            //db.SaveChanges();
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
