CREATE TABLE [dbo].[Sessions] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [SessionTitle]    NVARCHAR (50) NOT NULL,
    [FromDate]        DATETIME      NOT NULL,
    [ToDate]          DATETIME      NOT NULL,
    [CreatedBy]       INT           NOT NULL,
    [CreatedDate]     DATETIME      NOT NULL,
    [LastChangedBy]   INT           NOT NULL,
    [LastChangedDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Sessions_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Sessions_User_LastChangedBy] FOREIGN KEY ([LastChangedBy]) REFERENCES [dbo].[User] ([ID])
);

