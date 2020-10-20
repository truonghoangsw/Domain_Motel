CREATE TABLE [Auth].[Auth_Permission]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Permission] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](512) NULL,
	[Status] [tinyint] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[UpdatedTime] [datetime] NOT NULL,
	[Deleted] [tinyint] NOT NULL,
)
