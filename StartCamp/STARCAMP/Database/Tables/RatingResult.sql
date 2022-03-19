CREATE TABLE [dbo].[RatingResult]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [UserId] INT NOT NULL, 
    [QuestionId] INT NOT NULL, 
    [Rating] INT NOT NULL DEFAULT 0, 
    [Comment] NVARCHAR(4000) NULL, 
    [CommentDate] DATETIME NOT NULL,
	
)
Go

ALTER TABLE [RatingResult]
ADD FOREIGN KEY ([UserId]) REFERENCES [User](Id);
Go

ALTER TABLE [RatingResult]
ADD FOREIGN KEY ([QuestionId]) REFERENCES [SurveyQuestion](Id);
Go
