CREATE TABLE [dbo].[CommitteeAttendanceType] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Attendance]  NVARCHAR (220) NULL,
    [CreatedBy]   INT            NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_CommitteeAttendanceType] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CommitteeAttendanceType_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID])
);

