CREATE TABLE [dbo].[AbsenceRequestReason] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Reason]      NVARCHAR (255) NULL,
    [CreatedBy]   INT            NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_AbsenceRequestReason] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AbsenceRequestReason_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID])
);

