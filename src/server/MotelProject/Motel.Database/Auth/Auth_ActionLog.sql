
CREATE TABLE [Auth].[Auth_ActionLog](
	[ActionID] [int] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](128) NOT NULL,
	[UserID] [int] NOT NULL,
	[ObjectID] [int] NOT NULL,
	[ObjectType] [int] NOT NULL,
	[Data] [text] NULL,
	[CreatedTime] [datetime] NOT NULL,
	[Ip] [nvarchar](50) NOT NULL,
	[Result] [int] NOT NULL,
	[Message] [nvarchar](50) NULL,
	[RequestUrl] [nvarchar](50) NULL,
	[Device] [nvarchar](50) NULL,
	[HttpMethod] [nvarchar](50) NULL,
 CONSTRAINT [PK_Auth_ActionLog] PRIMARY KEY CLUSTERED 
(
	[ActionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
