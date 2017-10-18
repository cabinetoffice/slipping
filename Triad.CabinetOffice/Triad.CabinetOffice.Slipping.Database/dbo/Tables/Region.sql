CREATE TABLE [dbo].[Region] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Region]      NVARCHAR (50) NOT NULL,
    [CountryID]   INT           NOT NULL,
    [CreatedBy]   INT           NOT NULL,
    [CreatedDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UQ_Region] UNIQUE NONCLUSTERED ([Region] ASC),
    CONSTRAINT [FK_Region_Country_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Country] ([ID]),
    CONSTRAINT [FK_Region_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID])
);

