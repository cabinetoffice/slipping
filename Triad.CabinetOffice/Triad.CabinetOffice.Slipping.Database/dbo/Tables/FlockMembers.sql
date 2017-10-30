CREATE TABLE [dbo].[FlockMembers] (
    [ID]              INT      IDENTITY (1, 1) NOT NULL,
    [FlockID]         INT      NOT NULL,
    [MemberID]        INT      NOT NULL,
    [CreatedBy]       INT      NOT NULL,
    [CreatedDate]     DATETIME NOT NULL,
    [LastChangedBy]   INT      NOT NULL,
    [LastChangedDate] DATETIME NOT NULL,
    CONSTRAINT [PK_FlockMembers] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UQ_FlockMembers_Memeber] UNIQUE NONCLUSTERED ([MemberID] ASC),
    CONSTRAINT [UQ_FlockMembers_Flock] UNIQUE NONCLUSTERED ([FlockID] ASC, [MemberID] ASC),
    CONSTRAINT [FK_FlockMembers_Flock_FlockID] FOREIGN KEY ([FlockID]) REFERENCES [dbo].[Flock] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_FlockMembers_MembersOfParliament_MemberID] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[MembersOfParliament] ([ID]),
    CONSTRAINT [FK_FlockMembers_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_FlockMembers_User_LastChangedBy] FOREIGN KEY ([LastChangedBy]) REFERENCES [dbo].[User] ([ID])
);

