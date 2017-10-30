CREATE TABLE [dbo].[Committee] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (220) NOT NULL,
    [Subject]         NVARCHAR (220) NULL,
    [StartDate]       DATETIME       NOT NULL,
    [EndDate]         DATETIME       NOT NULL,
    [TypeID]          INT            NOT NULL,
    [CreatedBy]       INT            NOT NULL,
    [CreatedDate]     DATETIME       NOT NULL,
    [LastChangedBy]   INT            NOT NULL,
    [LastChangedDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_Committee] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Committee_CommitteeType_TypeID] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[CommitteeType] ([ID]),
    CONSTRAINT [FK_Committee_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Committee_User_LastChangedBy] FOREIGN KEY ([LastChangedBy]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [CK_Committee_Date] CHECK ([StartDate]<=[EndDate])
);

