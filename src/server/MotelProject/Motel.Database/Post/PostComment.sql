CREATE TABLE [dbo].[PostComment]
(
	[Id]  INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [RetenerId] INT NULL, 
    [CommentText] NVARCHAR(MAX) NULL, 
    [PostId ] INT NULL, 
    [CreatedOnUtc] DATETIME NULL,

)
