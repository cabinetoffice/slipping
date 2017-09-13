CREATE TABLE [dbo].[AbsenceRequestOppositionMP](
	[ID] INT IDENTITY(1,1) NOT NULL,
	[AbsenceRequestID] INT NOT NULL,
	[MPID] INT NULL,
	[MPFullName] NVARCHAR(255) NOT NULL,
	[CreatedBy] INT NOT NULL,
	[CreatedDate] DATETIME NOT NULL,
	[LastChangedBy] INT NOT NULL,
	[LastChangedDate] DATETIME NOT NULL,
	CONSTRAINT [PK_AbsenceRequestOppositionMP] PRIMARY KEY (ID),
	CONSTRAINT [FK_AbsenceRequestOppositionMP_AbsenceRequestID] FOREIGN KEY (AbsenceRequestID) REFERENCES [dbo].[AbsenceRequest](ID),
	CONSTRAINT [FK_AbsenceRequestOppositionMP_CreatedBy] FOREIGN KEY (CreatedBy) REFERENCES [dbo].[User](ID),
	CONSTRAINT [FK_AbsenceRequestOppositionMP_LastChangedBy] FOREIGN KEY (CreatedBy) REFERENCES [dbo].[User](ID)
 )