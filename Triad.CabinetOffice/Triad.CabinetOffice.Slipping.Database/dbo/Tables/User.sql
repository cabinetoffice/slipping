CREATE TABLE [dbo].[User]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[Username] VARCHAR(255) NOT NULL,
	[CreatedBy] INT NOT NULL,
	[CreatedDate] DATETIME NOT NULL,
	[Forenames] [nvarchar](255) NOT NULL,
	[Surname] [nvarchar](255) NOT NULL,
	[IsMP] [bit] NOT NULL,
	CONSTRAINT PK_User PRIMARY KEY (ID),
	CONSTRAINT UK_User_Username UNIQUE (Username),
	CONSTRAINT FK_User_User FOREIGN KEY (CreatedBy) REFERENCES [dbo].[User](ID)
)
