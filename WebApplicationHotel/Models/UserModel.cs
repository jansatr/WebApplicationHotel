using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationHotel.Models
{
    public class UserModel
    {
        public string EmailAddress { get; set; }
        public string ConfirmEmail { get; set; }
        public string Password { get; set; }
        public string  ConfirmPassword { get; set; }
    }
}