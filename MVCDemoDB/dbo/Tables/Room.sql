CREATE TABLE [dbo].[Room]
(
	[RoomId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RoomSize] INT NOT NULL, 
    [RoomPrice] DECIMAL NOT NULL, 
    [Description] VARCHAR(150) NULL
)
