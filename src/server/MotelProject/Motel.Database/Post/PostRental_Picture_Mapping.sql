CREATE TABLE [dbo].[PostRental_Picture_Mapping]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PictureId] [int] NOT NULL,
	[PostId] [int] NOT NULL,
	[DisplayOrder] [int] NOT NULL, 
    CONSTRAINT [FK_PostRental_Picture_Mapping_Picture] FOREIGN KEY (PictureId) REFERENCES Picture(Id),
	CONSTRAINT [FK_PostRental_Picture_Mapping_PostRental] FOREIGN KEY (PostId) REFERENCES RentalPost(Id)
)
