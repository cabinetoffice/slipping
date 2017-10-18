CREATE TABLE [dbo].[Votes] (
    [ID]              INT      IDENTITY (1, 1) NOT NULL,
    [DivisionID]      INT      NOT NULL,
    [MPID]            INT      NOT NULL,
    [Aye]             BIT      CONSTRAINT [DF_Votes_Aye] DEFAULT (CONVERT([bit],'False',(0))) NOT NULL,
    [No]              BIT      CONSTRAINT [DF_Votes_No] DEFAULT (CONVERT([bit],'False',(0))) NOT NULL,
    [CreatedBy]       INT      NOT NULL,
    [CreatedDate]     DATETIME NOT NULL,
    [LastChangedBy]   INT      NOT NULL,
    [LastChangedDate] DATETIME NOT NULL,
    CONSTRAINT [PK_Votes] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Votes_Divisions_DivisionID] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[Divisions] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Votes_MembersOfParliament_MPID] FOREIGN KEY ([MPID]) REFERENCES [dbo].[MembersOfParliament] ([ID]),
    CONSTRAINT [FK_Votes_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Votes_User_LastChangedBy] FOREIGN KEY ([LastChangedBy]) REFERENCES [dbo].[User] ([ID])
);

