CREATE TABLE [dbo].[Flock] (
    [ID]           INT      IDENTITY (1, 1) NOT NULL,
    [FlockOwnerID] INT      NOT NULL,
    [CreatedBy]    INT      NOT NULL,
    [CreatedDate]  DATETIME NOT NULL,
    CONSTRAINT [PK_Flock] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UQ_Flock] UNIQUE NONCLUSTERED ([FlockOwnerID] ASC),
    CONSTRAINT [FK_Flock_MembersOfParliament_FlockOwnerID] FOREIGN KEY ([FlockOwnerID]) REFERENCES [dbo].[MembersOfParliament] ([ID]),
    CONSTRAINT [FK_Flock_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID])
);

