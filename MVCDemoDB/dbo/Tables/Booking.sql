CREATE TABLE [dbo].[Booking]
(
	[BookingId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NOT NULL, 
    [BookingFrom] DATE NOT NULL, 
    [BookingTo] DATE NOT NULL, 
    [AssignedRoomId] INT NOT NULL, 
    [TotalAmount] DECIMAL NOT NULL
)
