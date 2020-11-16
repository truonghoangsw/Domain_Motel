USE [dbMotelV1]
GO

/****** Object:  Table [dbo].[Territories]    Script Date: 11/15/2020 5:18:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Territories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegionId] [tinyint] NULL,
	[Code] [nvarchar](16) NULL,
	[Name] [nvarchar](128) NULL,
	[Parent] [int] NULL,
	[Order] [int] NULL,
	[Slug] [nvarchar](128) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedTime] [datetime] NULL,
	[Deleted] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


