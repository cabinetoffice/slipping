CREATE TABLE [dbo].[CommitteeMeetingAttendance] (
    [ID]                 INT      IDENTITY (1, 1) NOT NULL,
    [CommitteeMeetingID] INT      NOT NULL,
    [CommitteeMemberID]  INT      NOT NULL,
    [AttendanceTypeID]   INT      NULL,
    [CreatedBy]          INT      NOT NULL,
    [CreatedDate]        DATETIME NOT NULL,
    [LastChangedBy]      INT      NOT NULL,
    [LastChangedDate]    DATETIME NOT NULL,
    CONSTRAINT [PK_CommitteeMeetingAttendance] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UQ_CommitteeMeetingAttend] UNIQUE NONCLUSTERED ([CommitteeMeetingID] ASC, [CommitteeMemberID] ASC),
    CONSTRAINT [FK_CommitteeMeetingAttendance_MembersOfParliament_CommitteeMemberID] FOREIGN KEY ([CommitteeMemberID]) REFERENCES [dbo].[MembersOfParliament] ([ID]),
    CONSTRAINT [FK_CommitteeMeetingAttendance_CommitteeMeeting_CommitteeMeetingID] FOREIGN KEY ([CommitteeMeetingID]) REFERENCES [dbo].[CommitteeMeeting] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_CommitteeMeetingAttendance_CommitteeAttendanceTypes_AttendanceTypeID] FOREIGN KEY ([AttendanceTypeID]) REFERENCES [dbo].[CommitteeAttendanceType] ([ID]),
    CONSTRAINT [FK_CommitteeMeetingAttendance_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_CommitteeMeetingAttendance_User_LastChangedBy] FOREIGN KEY ([LastChangedBy]) REFERENCES [dbo].[User] ([ID])
);

