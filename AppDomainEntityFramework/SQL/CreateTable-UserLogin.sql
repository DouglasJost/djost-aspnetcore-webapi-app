USE [MusicCollectionDB]
GO

/****** Object:  Table [dbo].[UserLogin]    Script Date: 11/22/2024 1:58:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserLogin](
	[UserAccountId] [uniqueidentifier] NOT NULL,
	[Inactive] [bit] NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[UserDefined] [bit] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[UserAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserLogin] ADD  CONSTRAINT [DF_UserLogin_Inactive]  DEFAULT ((0)) FOR [Inactive]
GO

ALTER TABLE [dbo].[UserLogin] ADD  CONSTRAINT [DF_UserLogin_UserDefined]  DEFAULT ((0)) FOR [UserDefined]
GO

ALTER TABLE [dbo].[UserLogin] ADD  CONSTRAINT [DF_UserLogin_LastModifiedDate]  DEFAULT (getutcdate()) FOR [LastModifiedDate]
GO

ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLogin_User] FOREIGN KEY([UserAccountId])
REFERENCES [dbo].[UserAccount] ([UserAccountId])
GO

ALTER TABLE [dbo].[UserLogin] CHECK CONSTRAINT [FK_UserLogin_User]
GO


