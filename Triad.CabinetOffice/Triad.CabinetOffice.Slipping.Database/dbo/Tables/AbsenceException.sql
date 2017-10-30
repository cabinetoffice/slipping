CREATE TABLE [dbo].[AbsenceException]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[Exception] NVARCHAR(220) NOT NULL,
    [CreatedBy] INT NOT NULL,
    [CreatedDate] DATETIME NOT NULL
	CONSTRAINT PK_AbsenceException PRIMARY KEY (ID)
)
