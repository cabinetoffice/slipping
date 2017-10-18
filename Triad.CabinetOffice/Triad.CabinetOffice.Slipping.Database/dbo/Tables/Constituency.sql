CREATE TABLE [dbo].[Constituency] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [Constituency] NVARCHAR (50) NOT NULL,
    [RegionID]     INT           NOT NULL,
    [CreatedBy]    INT           NOT NULL,
    [CreatedDate]  DATETIME      NOT NULL,
    CONSTRAINT [PK_Constituency] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UQ_Constituency] UNIQUE NONCLUSTERED ([Constituency] ASC),
    CONSTRAINT [FK_Constituency_Region_RegionID] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([ID]),
    CONSTRAINT [FK_Constituency_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID])
);

