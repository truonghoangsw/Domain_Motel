CREATE TABLE [Auth].[Auth_UsersLog]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ObjectCode] [nvarchar](50) NULL,
	[EntityID] [int] NOT NULL,
	[EntityType] [int] NOT NULL,
	[EntityTypeName] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NOT NULL,
	[Result] [int] NOT NULL,
	[Message] [nvarchar](128) NULL,
	[Infomation] [ntext] NULL,
	[IP] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserLog] PRIMARY KEY CLUSTERED 
([Id])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
