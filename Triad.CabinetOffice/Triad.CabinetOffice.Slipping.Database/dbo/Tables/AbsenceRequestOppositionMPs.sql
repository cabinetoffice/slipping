
CREATE TABLE [dbo].[AbsenceRequestOppositionMP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AbsenceRequestID] [int] NOT NULL,
	[MPID] [int] NULL,
	[MPFullName] [nvarchar](255) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastChangedBy] [int] NOT NULL,
	[LastChangedDate] [datetime] NOT NULL,
	CONSTRAINT [PK_AbsenceRequestOppositionMP] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_AbsenceRequestOppositionMP_AbsenceRequestID] FOREIGN KEY([AbsenceRequestID]) REFERENCES [dbo].[AbsenceRequest] ([ID]),
	CONSTRAINT [FK_AbsenceRequestOppositionMP_CreatedBy] FOREIGN KEY([CreatedBy]) REFERENCES [dbo].[User] ([ID]),
	CONSTRAINT [FK_AbsenceRequestOppositionMP_LastChangedBy] FOREIGN KEY([CreatedBy]) REFERENCES [dbo].[User] ([ID])
)




