using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationHotel.Models
{
    public class RoomModel
    {
        [Display(Name = "Kohtade arv")]
        [DataType(DataType.Date)]
        public int RoomSize { get; set; }

        [Display(Name = "Öö hind")]
        [DataType(DataType.Date)]
        public double RoomPrice { get; set; }
    }
}