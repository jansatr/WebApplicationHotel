using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class BookingModel
    {
        public int BookingId { get; set; }
        public int RoomId  { get; set; }

        public DateTime? BookingFrom { get; set; }

        public DateTime? BookingTo { get; set; }

        public int RoomSize { get; set; }

        public double RoomPrice { get; set; }

        public double TotalAmount { get; set; }

        public int CustomerId { get; set; }

        public DateTime? CreationDateTime { get; set; }

    }
}
