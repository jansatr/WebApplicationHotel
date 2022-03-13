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
        [Authorize]
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel model)
        {
            //if (ModelState.IsValid)
            {
                var data = UserProcessor.LoadUser(model.EmailAddress);
               
                var user = data.Where(u => u.Password.ToString().Trim() == model.Password.ToString()).FirstOrDefault();
                if (user != null)
                {
                    Session["UserID"] = (int)user.Id;
                    Session["UserName"] = user.FirstName.ToString();
                    return RedirectToAction("../Booking/Booking");
                }

            }
            return View(model);
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

    }
}