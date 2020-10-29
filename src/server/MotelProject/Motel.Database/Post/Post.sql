CREATE TABLE [dbo].[Post]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [TitlePost] NVARCHAR(150) NULL, 
    [LengthMotel] FLOAT NULL, 
    [WideMotel] FLOAT NULL, 
    [MonthlyPrice] INT NULL, 
    [AcreageDram] VARCHAR(50) NULL, 
    [DescriptionInformation] NVARCHAR(MAX) NULL, 
    [TypeRoomToilet ] INT NULL, 
    [CreateDate] DATETIME NULL, 
    [CreateBy] INT NULL, 
    [UpdateDate] DATETIME NULL, 
    [Status] TINYINT NULL, 
    [FurnitureInformation] NVARCHAR(MAX) NULL,
)
