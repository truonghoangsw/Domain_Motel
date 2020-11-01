CREATE TABLE [dbo].[PackageTypePost]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [NamePackage] NVARCHAR(150) NULL, 
    [PricePerDay] FLOAT NULL, 
    [AmountDay] INT NULL, 
    [TypePackage] NCHAR(10) NULL, 
    [CreateBy] INT NULL, 
    [CreatedTime] DATETIME NULL
)
