CREATE TABLE [dbo].[Log]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [LogLevelId] INT NULL, 
    [ShortMessage] NVARCHAR(MAX) NULL, 
    [FullMessage] NVARCHAR(MAX) NULL, 
    [IpAddress] NVARCHAR(50) NULL, 
    [CustomerId] INT NULL, 
    [PageUrl] NVARCHAR(MAX) NULL, 
    [ReferrerUrl] NVARCHAR(MAX) NULL, 
    [CreatedOnUtc] DATETIME NULL, 
    [LogLevel] INT NULL,
)
