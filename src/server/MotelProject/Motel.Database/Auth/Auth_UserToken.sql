CREATE TABLE [Auth].[Auth_UserToken]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [AccessTokenHash] VARCHAR(150) NULL, 
    [AccessTokenExpiresDateTime] DATETIMEOFFSET NULL, 
    [RefreshTokenIdHash] VARCHAR(150) NULL, 
    [RefreshTokenIdHashSource] VARCHAR(150) NULL, 
    [RefreshTokenExpiresDateTime] DATETIMEOFFSET NULL, 
    [UserId] INT NULL, 
    CONSTRAINT [FK_Auth_UserToken_Auth_User] FOREIGN KEY (UserId) REFERENCES [Auth].[Auth_User](Id)
)
