﻿CREATE TABLE [dbo].[Divisions] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [DivisionNumber]   INT            NOT NULL,
    [DivisionDateTime] DATETIME       NOT NULL,
    [Title]            NVARCHAR (255) NOT NULL,
    [GovtLine]         NVARCHAR (220) NOT NULL,
    [Ayes]             DECIMAL (18)   NULL,
    [Noes]             DECIMAL (18)   NULL,
    [DeferredDivision] BIT            CONSTRAINT [DF_Divisions_DeferredDivision] DEFAULT (CONVERT([bit],'False',(0))) NOT NULL,
    [SessionID]        INT            NOT NULL,
    [WhippingID]       INT            NOT NULL,
    [Subject]          NVARCHAR (220) NULL,
    [Teller1ForAyes]   INT            NULL,
    [Teller2ForAyes]   INT            NULL,
    [Teller1ForNoes]   INT            NULL,
    [Teller2ForNoes]   INT            NULL,
    [CreatedBy]        INT            NOT NULL,
    [CreatedDate]      DATETIME       NOT NULL,
    [LastChangedBy]    INT            NOT NULL,
    [LastChangedDate]  DATETIME       NOT NULL,
    CONSTRAINT [PK_Divisions] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Divisions_MembersOfParliament_Teller2ForNoes] FOREIGN KEY ([Teller2ForNoes]) REFERENCES [dbo].[MembersOfParliament] ([ID]),
    CONSTRAINT [FK_Divisions_MembersOfParliament_Teller1ForAyes] FOREIGN KEY ([Teller1ForAyes]) REFERENCES [dbo].[MembersOfParliament] ([ID]),
    CONSTRAINT [FK_Divisions_Whipping_WhippingID] FOREIGN KEY ([WhippingID]) REFERENCES [dbo].[Whipping] ([ID]),
    CONSTRAINT [FK_Divisions_MembersOfParliament_Teller2ForAyes] FOREIGN KEY ([Teller2ForAyes]) REFERENCES [dbo].[MembersOfParliament] ([ID]),
    CONSTRAINT [FK_Divisions_Sessions_SessionID] FOREIGN KEY ([SessionID]) REFERENCES [dbo].[Sessions] ([ID]),
    CONSTRAINT [FK_Divisions_MembersOfParliament_Teller1ForNoes] FOREIGN KEY ([Teller1ForNoes]) REFERENCES [dbo].[MembersOfParliament] ([ID]),
    CONSTRAINT [FK_Divisions_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Divisions_User_LastChangedBy] FOREIGN KEY ([LastChangedBy]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [CK_Divisions_Teller] CHECK ([Teller1ForAyes]<>[Teller2ForAyes] AND [Teller1ForAyes]<>[Teller1ForNoes] AND [Teller1ForAyes]<>[Teller2ForNoes] AND [Teller2ForAyes]<>[Teller1ForNoes] AND [Teller2ForAyes]<>[Teller2ForNoes] AND [Teller1ForNoes]<>[Teller2ForNoes]),
    CONSTRAINT [CK_Divisions_Title] CHECK (len([Title])>(1.0))
);

