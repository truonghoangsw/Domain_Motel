﻿
	CREATE TABLE [Auth].[Auth_UserRoles](
		[UserID] [int] NOT NULL,
		[RoleID] [int] NOT NULL,
	 [Id] INT NOT NULL IDENTITY, 
    CONSTRAINT [PK_Auth_UserRoles] PRIMARY KEY CLUSTERED 
	([Id])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	GO

