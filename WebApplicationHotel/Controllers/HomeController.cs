using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationHotel.Models;
using DataLibrary;
using DataLibrary.BusinessLogic;

namespace WebApplicationHotel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ViewUsers()
        {
            ViewBag.Message = "Users list.";
            var data = UserProcessor.LoadUsers();
            List<UserModel> users = new List<UserModel>();
            foreach (var user in data)
            {
                users.Add(new UserModel
                {
                    FirstName= user.FirstName,
                    LastName= user.LastName,
                    IdentityNumber= user.IdentityNumber,
                    EmailAddress = user.EmailAddress
                });
            }


            return View(users);
        }

        public ActionResult SignUp()
        {
            ViewBag.Message = "User sign up.";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserModel model)
        {
           if (ModelState.IsValid)
            {
                int recordsCreated=UserProcessor.CreateUser(model.EmailAddress,model.FirstName,model.LastName,model.IdentityNumber);
                return RedirectToAction("Index");
            }

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
                var data =BookingProcessor.LoadAvailableRooms(model.CheckInDay, model.CheckOutDay);
                List<BookingModel> rooms = new List<BookingModel>();
                foreach (var room in data)
                { 
                    
                    rooms.Add(new BookingModel
                    {
                        
                         RoomSize=room.RoomSize,
                         RoomPrice=room.RoomPrice
                         
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

        public ActionResult BookingDetails() {
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
                    RoomPriceTotal = Difference.Days * room.RoomPrice
                    // LastName = room.LastName,
                    // IdentityNumber = room.IdentityNumber,
                    // EmailAddress = room.EmailAddress
                }); ;
            }
            return View(rooms);
        }

    }
}