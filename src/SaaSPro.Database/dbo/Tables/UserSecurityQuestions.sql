CREATE TABLE [dbo].[UserSecurityQuestions] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Question] NVARCHAR (255)   NULL,
    [Answer]   NVARCHAR (255)   NULL,
    [UserId]   UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SecurityQuestions_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON UPDATE CASCADE ON DELETE CASCADE
);

