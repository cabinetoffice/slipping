CREATE TABLE [dbo].[Whipping] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Whipping]    NVARCHAR (255) NULL,
    [CreatedBy]   INT            NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_Whipping] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Whipping_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID])
);

