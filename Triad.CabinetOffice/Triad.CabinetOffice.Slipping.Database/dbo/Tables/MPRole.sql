CREATE TABLE [dbo].[MPRole] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [MPRole]      NVARCHAR (255) NULL,
    [CreatedBy]   INT            NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_MPRole] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_MPRole_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID])
);

