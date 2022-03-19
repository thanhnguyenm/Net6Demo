CREATE TABLE [dbo].[SurveyQuestion]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Question ] NVARCHAR(4000) NULL, 
    [ServiceId] INT NOT NULL
	
);
Go

ALTER TABLE [SurveyQuestion]
ADD FOREIGN KEY ([ServiceId]) REFERENCES Service(Id);