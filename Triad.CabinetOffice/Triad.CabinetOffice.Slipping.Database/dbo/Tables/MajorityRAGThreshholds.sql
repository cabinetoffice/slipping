CREATE TABLE [dbo].[MajorityRAGThreshholds] (
    [ID]          INT      IDENTITY (1, 1) NOT NULL,
    [Red]         INT      CONSTRAINT [DF_MajorityRAGThreshholds_Red] DEFAULT ((0.0)) NULL,
    [Amber]       INT      CONSTRAINT [DF_MajorityRAGThreshholds_Amber] DEFAULT ((0.0)) NOT NULL,
    [CreatedBy]   INT      NOT NULL,
    [CreatedDate] DATETIME NOT NULL,
    CONSTRAINT [PK_MajorityRAGThreshholds] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_MajorityRAGThreshholds_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [CK_MajorityRAGThreshholds_Amber_Red] CHECK ([Amber]>(0.0) AND [Amber]>[Red]),
    CONSTRAINT [CK_MajorityRAGThreshholds_Red] CHECK ([Red]>(0.0))
);

