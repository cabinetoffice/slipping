CREATE TABLE [dbo].[MPStatus] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [MPStatus]    NVARCHAR (255) NULL,
    [CreatedBy]   INT            NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_MPStatus] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_MPStatus_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID])
);

