CREATE TABLE [dbo].[Country] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (50) NOT NULL,
    [CreatedBy]   INT           NOT NULL,
    [CreatedDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UQ_Country] UNIQUE NONCLUSTERED ([Title] ASC),
    CONSTRAINT [FK_Country_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID])
);

