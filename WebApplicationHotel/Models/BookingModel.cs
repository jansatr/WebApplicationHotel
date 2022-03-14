using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationHotel.Models
{
    public class BookingModel
    {
        public int RoomId { get; set; }

        [Display(Name = "Alates")]
        [DataType(DataType.Date)]
        public DateTime? CheckInDay { get; set; }

        [Display(Name = "Kuni")]
        [DataType(DataType.Date)]
        public DateTime? CheckOutDay { get; set; }
        //public int StudentId { get; set; }
        //[Display(Name = "From Date")]
        //[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime FromDate { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        //[Display(Name = "To Date")]
        //public DateTime ToDate { get; set; }
        [Display(Name = "Kohtade arv")]
        //[DataType(DataType.Date)]
        public int RoomSize { get; set; }

        [Display(Name = "Öö hind")]
        //[DataType(DataType.Date)]
        public double RoomPrice { get; set; }

        [Display(Name = "Hind kokku")]
        //[DataType(DataType.Date)]
        public double RoomPriceTotal { get; set; }

        public int UserId { get; set; }      

        public int BookingId { get; set; }

        [Display(Name = "Toa number")]
        public int RoomNumber { get; set; }
        [Display(Name = "Eesnimi")]
        public string FirstName { get; set; }
        [Display(Name = "Perekonnanimi")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }




    }
}