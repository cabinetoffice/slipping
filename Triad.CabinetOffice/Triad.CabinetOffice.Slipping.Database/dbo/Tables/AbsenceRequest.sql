CREATE TABLE [dbo].[AbsenceRequest]
(
	[ID] INT IDENTITY(1,1) NOT NULL,
	[MPID] INT NOT NULL,
	[ReasonID] INT NULL,
	[Details] NVARCHAR(220) NULL,
	[StatusID] INT NOT NULL,
	[FromDate] DATETIME NOT NULL,
	[ToDate] DATETIME NULL,
	[DecisionNotes] NVARCHAR(220) NULL,
	[CreatedBy] INT NOT NULL,
	[CreatedDate] DATETIME NOT NULL,
	[LastChangedBy] INT NOT NULL,
	[LastChangedDate] DATETIME NOT NULL
	CONSTRAINT PK_AbsenceRequest PRIMARY KEY (ID),
	CONSTRAINT FK_AbsenceRequest_User_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES [dbo].[User](ID),
	CONSTRAINT FK_AbsenceRequest_User_LastChangedBy FOREIGN KEY (LastChangedBy) REFERENCES [dbo].[User](ID)
)
