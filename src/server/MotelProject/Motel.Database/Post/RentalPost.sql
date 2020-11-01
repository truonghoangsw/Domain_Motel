CREATE TABLE [dbo].[RentalPost]
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
    [TerritoriesId] INT NULL, 
    [LesterId] INT NULL, 
    [Tag] VARCHAR(10) NULL, 
    CONSTRAINT [FK_Post_Territories] FOREIGN KEY (TerritoriesId) REFERENCES Territories(Id),
    CONSTRAINT [FK_Post_Lesters] FOREIGN KEY (LesterId) REFERENCES Lesters(Id),
)

GO
