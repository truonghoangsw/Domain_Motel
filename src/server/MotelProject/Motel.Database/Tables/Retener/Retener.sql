CREATE TABLE [dbo].[Retener]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Mobile] [nvarchar](15) NOT NULL,
	[Salt] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Birthday] [date] NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Gender] [tinyint] NULL,
	[Address] [nvarchar](255) NULL,
	[AddressType] [tinyint] NULL,
	[AccountName] [nvarchar](256) NULL,
	[Deleted] [tinyint] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[IdentityCard] [nvarchar](32) NULL,
	[IdentityDay] [date] NULL,
	[StatusId] [tinyint] NULL,
	[UserId] [int] Not Null, 
    [TerritoriesId] INT NULL, 
    CONSTRAINT [FK_Retener_Auth_User] FOREIGN KEY (UserId) REFERENCES [dbo].[Auth_User]([Id]),
	CONSTRAINT [FK_Retener_Territories] FOREIGN KEY (TerritoriesId) REFERENCES Territories([Id])
)
