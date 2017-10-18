CREATE TABLE [dbo].[CommitteeType] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Type]        NVARCHAR (220) NULL,
    [CreatedBy]   INT            NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_CommitteeType] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CommitteeType_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID])
);

