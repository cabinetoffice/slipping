CREATE TABLE [dbo].[CommitteeMeeting] (
    [ID]              INT      IDENTITY (1, 1) NOT NULL,
    [CommitteeID]     INT      NOT NULL,
    [StartDate]       DATETIME NOT NULL,
    [EndDate]         DATETIME NOT NULL,
    [CreatedBy]       INT      NOT NULL,
    [CreatedDate]     DATETIME NOT NULL,
    [LastChangedBy]   INT      NOT NULL,
    [LastChangedDate] DATETIME NOT NULL,
    CONSTRAINT [PK_CommitteeMeeting] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CommitteeMeeting_Committee_CommitteeID] FOREIGN KEY ([CommitteeID]) REFERENCES [dbo].[Committee] ([ID]),
    CONSTRAINT [FK_CommitteeMeeting_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_CommitteeMeeting_User_LastChangedBy] FOREIGN KEY ([LastChangedBy]) REFERENCES [dbo].[User] ([ID])
);

