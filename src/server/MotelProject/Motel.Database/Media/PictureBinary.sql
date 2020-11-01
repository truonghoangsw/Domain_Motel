CREATE TABLE [dbo].[PictureBinary]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PictureId] [int] NOT NULL,
	[BinaryData] [varbinary](max) NULL, 
    CONSTRAINT [PK_PictureBinary] PRIMARY KEY ([Id])
)
