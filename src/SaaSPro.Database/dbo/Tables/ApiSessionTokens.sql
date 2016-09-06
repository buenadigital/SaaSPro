CREATE TABLE [dbo].[ApiSessionTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[Token] [nvarchar](255) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[QuestionAnswered] [bit] NOT NULL DEFAULT 0,
	[ExpirationDate] [datetime] NOT NULL,
	[CreatedBy] UNIQUEIDENTIFIER NULL,
	[CreatedOn] DATETIME NULL DEFAULT GETDATE(), 
	[UpdatedBy] UNIQUEIDENTIFIER NULL, 
	[UpdatedOn] DATETIME NULL DEFAULT GETDATE(),
	[SecurityQuestionId] [uniqueidentifier] NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_ApiSessionTokens_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT [FK_ApiSessionTokens_UserSecurityQuestions] FOREIGN KEY ([SecurityQuestionId]) REFERENCES [dbo].[UserSecurityQuestions] ([Id]) 
 )