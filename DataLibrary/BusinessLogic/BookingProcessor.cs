﻿using System;
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
        public static int CreateBooking(int roomId, DateTime? bookingFrom, DateTime? bookingTo, int customerId, double totalAmount, DateTime? creationDateTime)
        {
             BookingModel data = new BookingModel
            {
                        
                RoomId =roomId,

                BookingFrom =bookingFrom,

                BookingTo=bookingTo,

                CustomerId=customerId,

                TotalAmount=totalAmount,

                CreationDateTime=creationDateTime
                

            };
            //string sql = @"insert into dbo.User (EmailAddress) values(@EmailAddress);";
            string sql = @"insert into [dbo].[Booking] (AssignedRoomId, CustomerId, BookingFrom, BookingTo, TotalAmount,CreationDateTime) values (@roomId, @customerId, @bookingFrom, @bookingTo, @totalAmount, @creationDateTime);";
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
            
            string sql = @"SELECT    [dbo].[Room].RoomSize, [dbo].[Room].RoomPrice, [dbo].[Room].Description, [dbo].[Room].RoomId
                            FROM      Room
                            WHERE     RoomId NOT IN (
                                         SELECT    Booking.AssignedRoomId
                                         FROM      Booking
                                         WHERE     (
                                                      Booking.BookingFrom >= @BookingFromDate AND
                                                      Booking.BookingTo <= @BookingFromDate
                                                   ) OR (
                                                      Booking.BookingFrom <= @BookingToDate AND
                                                      Booking.BookingTo >= @BookingToDate
						                              )
                                                   );";

            //new System.Data.SqlClient.SqlParameter { ParameterName = "BookingFrom", Value = '2022-03-25' };
           // return SqlDataAccess.SaveData(sql, data);
            return SqlDataAccess.LoadRoomsData<BookingModel>(sql, ArrivalDate, DepartureDate);
            //eturn SqlDataAccess.LoadRoomsData<BookingModel>(sql, new { BookingFromDate = data.BookingFrom??DateTime.Now }, new { BookingToDate = data.BookingTo??DateTime.Now });

        }

        public static List<BookingModel> LoadRooms() {

            string sql = @"SELECT [dbo].[Room].RoomSize, [dbo].[Room].RoomPrice, [dbo].[Room].Description, [dbo].[Room].RoomId
                            FROM Room;";

            return SqlDataAccess.LoadData<BookingModel>(sql);

        }
        public static List<BookingModel> LoadRoom(int? id)
        {

            string sql = @"SELECT [dbo].[Room].RoomSize, [dbo].[Room].RoomPrice, [dbo].[Room].Description, [dbo].[Room].RoomId
                            FROM Room WHERE [dbo].[Room].RoomId=@param;";

            return SqlDataAccess.LoadDataWithParam<BookingModel>(sql, id);

        }

        public static List<BookingModel> LoadOwnBookings(int? id)
        {

            string sql = @"SELECT *FROM Booking INNER JOIN[dbo].[Room] ON[dbo].[Booking].AssignedRoomId =[dbo].[Room].RoomId
                    where CustomerId = @param and BookingFrom >= CAST(GETDATE() AS Date);";

            return SqlDataAccess.LoadDataWithParam<BookingModel>(sql, id);

        }
        public static List<BookingModel> LoadOwnOneBooking(int? id)
        {

            string sql = @"SELECT *FROM Booking INNER JOIN[dbo].[Room] ON[dbo].[Booking].AssignedRoomId =[dbo].[Room].RoomId
                    where BookingId = @param;";

            return SqlDataAccess.LoadDataWithParam<BookingModel>(sql, id);

        }

        public static int DeleteBooking(int? bookingId)
        {
            BookingModel data = new BookingModel
            {

                BookingId = (int)bookingId

            };
            string sql= @"DELETE from dbo.booking where BookingId = @BookingId;";

            return SqlDataAccess.DeleteData<BookingModel>(sql, data);

        }

    }
}
