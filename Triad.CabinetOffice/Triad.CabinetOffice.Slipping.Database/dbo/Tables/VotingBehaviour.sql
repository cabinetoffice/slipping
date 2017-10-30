CREATE TABLE [dbo].[VotingBehaviour] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Behaviour]   NVARCHAR (50) NULL,
    [CreatedBy]   INT           NOT NULL,
    [CreatedDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_VotingBehaviour] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_VotingBehaviour_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID])
);

