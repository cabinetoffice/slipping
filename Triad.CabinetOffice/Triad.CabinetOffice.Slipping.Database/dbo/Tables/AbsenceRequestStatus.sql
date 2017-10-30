CREATE TABLE [dbo].[AbsenceRequestStatus] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Status]      NVARCHAR (255) NULL,
    [CreatedBy]   INT            NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_AbsenceRequestStatus] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AbsenceRequestStatus_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID])
);

