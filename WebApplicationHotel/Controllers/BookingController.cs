using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationHotel.Models;
using DataLibrary;
using DataLibrary.BusinessLogic;
using System.Net;

namespace WebApplicationHotel.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Booking()
        {
            ViewBag.Message = "Toa broneerimine";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Booking(BookingModel model)
        {
            if (ModelState.IsValid)
            {
                var data = BookingProcessor.LoadAvailableRooms(model.CheckInDay, model.CheckOutDay);
                List<BookingModel> rooms = new List<BookingModel>();
                foreach (var room in data)
                {

                    rooms.Add(new BookingModel
                    {

                        RoomSize = room.RoomSize,
                        RoomPrice = room.RoomPrice,
                        RoomId=room.RoomId
                        // LastName = room.LastName,
                        // IdentityNumber = room.IdentityNumber,
                        // EmailAddress = room.EmailAddress
                    });
                }
                TempData["loginModel"] = model;
                ViewBag.CheckinDay = model.CheckInDay;
                ViewBag.CheckOutDay = model.CheckOutDay;
                //Response.Write(rooms.ToString().);
                //return View("Details");
                return RedirectToAction("BookingDetails");
            }
            ViewBag.Message = "Users list.";

            return View();

            //return View();

        }

        public ActionResult BookingDetails()
        {
            var model = TempData["loginModel"] as BookingModel;
            //ViewBag.Message = "Welcome " + model.CheckInDay;
            var data = BookingProcessor.LoadAvailableRooms(model.CheckInDay, model.CheckOutDay);
            List<BookingModel> rooms = new List<BookingModel>();
            foreach (var room in data)
            {
                TimeSpan Difference = ((TimeSpan)(model.CheckOutDay - model.CheckInDay));
                rooms.Add(new BookingModel
                {
                    RoomSize = room.RoomSize,
                    RoomPrice = room.RoomPrice,
                    RoomPriceTotal = Difference.Days * room.RoomPrice,
                    RoomId=room.RoomId
                    // LastName = room.LastName,
                    // IdentityNumber = room.IdentityNumber,
                    // EmailAddress = room.EmailAddress
                }); ;
            }
            return View(rooms);
        }
        //public ActionResult Details()
        //{
        //    ViewBag.Message = "Details page.";

        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Details(int? id)
        {
            var data = BookingProcessor.LoadRoom();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}


            List<BookingModel> rooms = new List<BookingModel>();
            foreach (var room in data)
            {
                if (room.RoomId == id)
                {
                    rooms.Add(new BookingModel
                    {
                        RoomSize = room.RoomSize,
                        RoomPrice = room.RoomPrice
                    }); ;
                }

            }
            return View(rooms);
        }

        public ActionResult BookingDone()
        {
            ViewBag.Message = "User sign up.";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookingDone(BookingModel model)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = BookingProcessor.CreateBooking(model.RoomId, model.CheckInDay, model.CheckOutDay, model.UserId, model.RoomPriceTotal);
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}