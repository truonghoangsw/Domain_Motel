CREATE TABLE [dbo].[PostImage]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [PostId] INT NULL, 

    [Path] VARCHAR(150) NULL, 
    [CreateTime] DATETIME NULL, 
    [CreateBy] INT NULL, 
    [UpdateTime] DATETIME NULL, 
    [Status] TINYINT NULL, 
    CONSTRAINT [FK_PostImage_Post] FOREIGN KEY ([PostId]) REFERENCES Post(Id), 

)
