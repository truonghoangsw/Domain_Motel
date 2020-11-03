CREATE TABLE [dbo].[RentalPost_TagMapping]
(
	[RentalPostId] [int] NOT NULL,
	[PostTagId] [int] NOT NULL, 
    CONSTRAINT [FK_RentalPost_TagMapping_RentalPost] FOREIGN KEY ([RentalPostId]) REFERENCES RentalPost(Id),
	CONSTRAINT [FK_RentalPost_TagMapping_PostTag] FOREIGN KEY ([PostTagId]) REFERENCES PostTag(Id)
)
