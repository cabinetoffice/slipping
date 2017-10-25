CREATE TABLE [dbo].[AbsenceRequestExceptions]
(
	[ID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[AbsenceRequestID] INT NOT NULL,
	[ExceptionID] INT NOT NULL,
	[OppositionMPID] INT NULL,
	[Notes] NVARCHAR(MAX) NULL,
	[FromDate] DATETIME NOT NULL,
	[ToDate] DATETIME NOT NULL,
    [CreatedBy] INT NOT NULL,
	[CreatedDate] DATETIME NOT NULL,
	[LastChangedBy] INT NOT NULL,
	[LastChangedDate] DATETIME NOT NULL
	CONSTRAINT PK_AbsenceRequestExceptions PRIMARY KEY (ID)
	CONSTRAINT FK_AbsenceRequestExceptions_AbsenceRequest FOREIGN KEY (AbsenceRequestID) REFERENCES [dbo].[AbsenceRequest](ID)
	CONSTRAINT FK_AbsenceRequestExceptions_AbsenceException FOREIGN KEY (ExceptionID) REFERENCES [dbo].[AbsenceException](ID)
	CONSTRAINT FK_AbsenceRequestExceptions_MembersOfParliament FOREIGN KEY (OppositionMPID) REFERENCES [dbo].[MembersOfParliament](ID)
)
