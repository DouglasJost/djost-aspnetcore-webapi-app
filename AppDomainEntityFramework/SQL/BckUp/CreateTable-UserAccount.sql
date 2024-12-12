USE [MusicCollectionDB]
GO

/****** Object:  Table [dbo].[UserAccount]    Script Date: 11/22/2024 1:58:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserAccount](
	[UserAccountId] [uniqueidentifier] NOT NULL,
	[Inactive] [bit] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[UserDefined] [bit] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserAccount] ADD  CONSTRAINT [DF_User_UserId]  DEFAULT (newid()) FOR [UserAccountId]
GO

ALTER TABLE [dbo].[UserAccount] ADD  CONSTRAINT [DF_User_Inactive]  DEFAULT ((0)) FOR [Inactive]
GO

ALTER TABLE [dbo].[UserAccount] ADD  CONSTRAINT [DF_User_UserDefined]  DEFAULT ((0)) FOR [UserDefined]
GO

ALTER TABLE [dbo].[UserAccount] ADD  CONSTRAINT [DF_User_LastModifiedDate]  DEFAULT (getutcdate()) FOR [LastModifiedDate]
GO


