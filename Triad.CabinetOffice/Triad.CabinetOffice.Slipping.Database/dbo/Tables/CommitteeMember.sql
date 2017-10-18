CREATE TABLE [dbo].[CommitteeMember] (
    [ID]              INT      IDENTITY (1, 1) NOT NULL,
    [CommitteeID]     INT      NOT NULL,
    [MemberID]        INT      NOT NULL,
    [CreatedBy]       INT      NOT NULL,
    [CreatedDate]     DATETIME NOT NULL,
    [LastChangedBy]   INT      NOT NULL,
    [LastChangedDate] DATETIME NOT NULL,
    CONSTRAINT [PK_CommitteeMember] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UQ_CommitteeMember] UNIQUE NONCLUSTERED ([CommitteeID] ASC, [MemberID] ASC),
    CONSTRAINT [FK_CommitteeMember_Committee_CommitteeID] FOREIGN KEY ([CommitteeID]) REFERENCES [dbo].[Committee] ([ID]),
    CONSTRAINT [FK_CommitteeMember_MembersOfParliament_MemberID] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[MembersOfParliament] ([ID]),
    CONSTRAINT [FK_CommitteeMember_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_CommitteeMember_User_LastChangedBy] FOREIGN KEY ([LastChangedBy]) REFERENCES [dbo].[User] ([ID])
);

