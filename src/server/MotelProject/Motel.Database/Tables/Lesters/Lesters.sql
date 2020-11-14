CREATE TABLE [dbo].[Lesters]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Mobile] [nvarchar](15) NOT NULL,
	[Salt] [nvarchar](150) NULL,
	[Password] [nvarchar](150) NULL,
	[Birthday] [date] NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Gender] [int] NULL,
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
    [FacebookId] VARCHAR(150) NULL, 
    CONSTRAINT [FK_Lesters_Auth_User] FOREIGN KEY (UserId) REFERENCES [dbo].[Auth_User]([Id])
)

