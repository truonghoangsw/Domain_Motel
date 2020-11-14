CREATE TABLE [dbo].[Auth_Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL ,
	[Name] [nvarchar](50) NOT NULL,
	[Desc] [nvarchar](255) NULL,
	[Status] [tinyint] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[UpdatedTime] [datetime] NOT NULL,
	[Deleted] [tinyint] NOT NULL,
	[IsSysAdmin] [tinyint] NOT NULL,
	[IsShow] [tinyint] NOT NULL,
 CONSTRAINT [PK_AuthRoles] PRIMARY KEY CLUSTERED 
([Id])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
