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
                int recordsCreated=UserProcessor.CreateUser(model.EmailAddress);
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}