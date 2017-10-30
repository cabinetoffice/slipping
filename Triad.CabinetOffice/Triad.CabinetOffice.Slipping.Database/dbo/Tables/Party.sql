CREATE TABLE [dbo].[Party] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Party]       NVARCHAR (10) NULL,
    [PartyName]   NVARCHAR (50) NULL,
    [GovtFlag]    BIT           CONSTRAINT [DF_Party] DEFAULT (CONVERT([bit],'False',(0))) NOT NULL,
    [CreatedBy]   INT           NOT NULL,
    [CreatedDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_Party] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Party_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID])
);

