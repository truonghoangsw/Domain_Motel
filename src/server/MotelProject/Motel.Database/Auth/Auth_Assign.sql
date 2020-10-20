CREATE TABLE [Auth].[Auth_Assign]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Permission] [nvarchar](50) NOT NULL,
	[ObjectID] [int] NOT NULL,
	[ObjectType] [tinyint] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
)
