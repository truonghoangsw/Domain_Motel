
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](400) NOT NULL,
	[MetaKeywords] [nvarchar](400) NULL,
	[MetaTitle] [nvarchar](400) NULL,
	[PriceRanges] [nvarchar](400) NULL,
	[PageSizeOptions] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[CategoryTemplateId] [int] NOT NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[ParentCategoryId] [int] NOT NULL,
	[PictureId] [int] NOT NULL,
	[PageSize] [int] NOT NULL,
	[AllowCustomersToSelectPageSize] [bit] NOT NULL,
	[ShowOnHomepage] [bit] NOT NULL,
	[IncludeInTopMenu] [bit] NOT NULL,
	[SubjectToAcl] [bit] NOT NULL,
	[LimitedToStores] [bit] NOT NULL,
	[Published] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
	[UpdatedOnUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

