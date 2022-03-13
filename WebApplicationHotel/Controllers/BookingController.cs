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
            ViewBag.Message = TempData["Notification"];
            return View();
        }

        public ActionResult Booking()
        {
            ViewBag.Message = "Toa broneerimine";

            return View();
        }

        //public ActionResult Delete()
        //{
        //    ViewBag.Message = "Toa broneerimine";

        //    return View();
        //}

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            var data = BookingProcessor.LoadOwnOneBooking(id);
       
            var booking = data.Where(b => b.BookingId == id).FirstOrDefault();
            DateTime bookingFromWHM = (DateTime)booking.BookingFrom;
            bookingFromWHM.AddHours(15);
            var hours= Math.Abs((DateTime.Now - bookingFromWHM).TotalHours);
            //if ()
            if (hours < 72)
                {
                TempData["Notification"]="Broneeringuid, mille algustähtajani on vähem kui 3 päeva, ei ole võimalik tühistada";
                 
                return RedirectToAction("Index");
                }
            //if (booking.BookingFrom)
            TimeSpan Difference = ((TimeSpan)(booking.BookingTo - booking.BookingFrom));
            var bookings = new BookingModel();
            bookings.RoomId = booking.RoomId;
            bookings.RoomPrice = booking.RoomPrice;
            bookings.BookingId = booking.BookingId;
            bookings.CheckOutDay = booking.BookingFrom;
            bookings.CheckInDay = booking.BookingTo;
            bookings.RoomSize = booking.RoomSize;
            bookings.RoomPriceTotal = Difference.Days * booking.RoomPrice;

            if (bookings == null)
            {
                return HttpNotFound();
            }
            

            return View(bookings);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid) 
            {
                int recordsCreated = BookingProcessor.DeleteBooking(id);
                return RedirectToAction("OwnBookingList");
            }

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
            if (ModelState.IsValid)
            { 
                var data = BookingProcessor.LoadAvailableRooms(model.CheckInDay, model.CheckOutDay);
                ViewBag.CheckInDay = (model.CheckInDay.Value.Date.ToString("dd-MMM-yyyy"));
                ViewBag.CheckOutDay=model.CheckOutDay.Value.Date.ToString("dd-MMM-yyyy");
                TempData["loginModel"] = model;
                Session["CheckInDay"] = model.CheckInDay;
                Session["CheckOutDay"] = model.CheckOutDay;
                List<BookingModel> rooms = new List<BookingModel>();
                foreach (var room in data)
                {
                    TimeSpan Difference = ((TimeSpan)(model.CheckOutDay - model.CheckInDay));
                    Session["Days"] = Difference.Days;
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
            return View();
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

            
            int? session = (int?)Session["UserID"];
            if (session == null) { 
                return RedirectToAction("../home/login");
            }

            ViewBag.Days = Session["Days"];
            var model = TempData["loginModel"] as BookingModel;
            var data = BookingProcessor.LoadRoom(id);
            var room=data.Where(r=> r.RoomId == id).FirstOrDefault();
            var rooms = new BookingModel();
            rooms.RoomId = room.RoomId;
            rooms.RoomPrice = room.RoomPrice;
            rooms.UserId = (int)Session["UserID"];
            rooms.CheckInDay = (DateTime?)Session["CheckInDay"];
            rooms.CheckOutDay = model.CheckOutDay;
            rooms.RoomPriceTotal= ViewBag.Days * room.RoomPrice;
            rooms.RoomSize = room.RoomSize;
            
            return View(rooms);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(BookingModel model)
        {
            DateTime CurrentDateTime = DateTime.Now;
            
                if (ModelState.IsValid)
            {
                int recordsCreated = BookingProcessor.CreateBooking(model.RoomId, model.CheckInDay, model.CheckOutDay, model.UserId, model.RoomPriceTotal, CurrentDateTime);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult OwnBookingList()
        {
            int? id=(int?) Session["UserID"];
            if (id == null)
            {
                return RedirectToAction("../home/login");
            }
            ViewBag.Message = "Users list.";
            var data = BookingProcessor.LoadOwnBookings(id);;
            List<BookingModel> Bookings = new List<BookingModel>();
            foreach (var booking in data)
            {
                TimeSpan Difference = ((TimeSpan)(booking.BookingTo - booking.BookingFrom));
                Bookings.Add(new BookingModel
                {
                    CheckInDay = booking.BookingFrom,
                    CheckOutDay = booking.BookingTo,
                    RoomSize = booking.RoomSize,
                    RoomPrice = booking.RoomPrice,
                    RoomPriceTotal = Difference.Days * booking.RoomPrice,
                    BookingId = booking.BookingId
                }); ;
            }


            return View(Bookings);
        }



    }
}