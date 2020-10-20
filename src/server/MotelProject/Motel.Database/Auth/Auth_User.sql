﻿CREATE TABLE [Auth].[Auth_User]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[UserName]             NVARCHAR (256)     NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    [LastRequestUtc]       DATETIME2 (7)      NOT NULL,
    [CreatedBy] [int] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[UpdatedTime] [datetime] NOT NULL,
    [Status] [tinyint] NOT NULL,
    [Avatar] [nvarchar](255) NULL,
    [Deleted] [tinyint] NOT NULL
)
