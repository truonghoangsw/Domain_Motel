CREATE TABLE [dbo].[Lester]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FullName] NVARCHAR(150) NULL, 
    [DateOfBirth] DATETIME NULL, 
    [IdentityCard] VARCHAR(20) NULL, 
    [Email] NVARCHAR(150) NULL, 
    [Address] NVARCHAR(250) NULL, 
    [NumberPhone] VARCHAR(50) NULL, 
    [IdentityDay] DATETIME NULL, 
    [AccountName] NVARCHAR(50) NULL, 
    [AvatarUrl] NVARCHAR(150) NULL, 
    [UserId] INT NULL, 
    CONSTRAINT [FK_Lester_Auth_User] FOREIGN KEY (UserId) REFERENCES Auth.Auth_User(Id)
)
