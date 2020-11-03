CREATE TABLE [dbo].[Territories]
(
	[Id] INT NOT NULL PRIMARY KEY  IDENTITY(1,1),
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
    [ProvincialId] INT NULL, 
    [DistrictId] INT NULL, 
    [WardId] INT NULL, 
    [AddressDetail] NVARCHAR(MAX) NULL, 
    [NumberRoom] NCHAR(10) NULL
)
