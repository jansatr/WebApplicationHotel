using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;
using DataLibrary.Models;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace DataLibrary.BusinessLogic
{
    public static class BookingProcessor

    {
        public static int CreateBooking(int roomId, DateTime bookingFrom, DateTime bookingTo, int customerId, double totalAmount)
        {
             BookingModel data = new BookingModel
            {
                        
                RoomId =roomId,

                BookingFrom =bookingFrom,

                BookingTo=bookingTo,

                CustomerId=customerId,

                TotalAmount=totalAmount

            };
            //string sql = @"insert into dbo.User (EmailAddress) values(@EmailAddress);";
            string sql = @"insert into [dbo].[Booking] (AssignedRoomId, CustomerId, BookingFrom, BookingTo, TotalAmount) values (@AssignedRoomId, @CustomerId, @BookingFrom, @BookingTo, @TotalAmount);";
            return SqlDataAccess.SaveData(sql, data);
        }
        public static List<BookingModel>  LoadAvailableRooms(DateTime? ArrivalDate,DateTime? DepartureDate)
        //
        {
            BookingModel data = new BookingModel
            {

                BookingFrom = ArrivalDate??DateTime.Now,

                BookingTo = DepartureDate??DateTime.Now

            };
            
            string sql = @"SELECT [dbo].[Room].RoomSize, [dbo].[Room].RoomPrice, [dbo].[Room].Description
                            FROM Booking
                            INNER JOIN [dbo].[Room] ON [dbo].[Booking].AssignedRoomId=[dbo].[Room].RoomId
                            WHERE (([dbo].[Booking].BookingFrom > @BookingFromDate AND [dbo].[Booking].BookingFrom >= @BookingToDate) 
                            OR ([dbo].[Booking].BookingTo <= @BookingFromDate AND [dbo].[Booking].Bookingto > @BookingToDate)
                            OR ([dbo].[Booking].BookingTo <= @BookingFromDate AND [dbo].[Booking].Bookingto <= @BookingFromDate));";

            //new System.Data.SqlClient.SqlParameter { ParameterName = "BookingFrom", Value = '2022-03-25' };
           // return SqlDataAccess.SaveData(sql, data);
            return SqlDataAccess.LoadRoomsData<BookingModel>(sql, ArrivalDate, DepartureDate);
            //eturn SqlDataAccess.LoadRoomsData<BookingModel>(sql, new { BookingFromDate = data.BookingFrom??DateTime.Now }, new { BookingToDate = data.BookingTo??DateTime.Now });

        }
    }
}
