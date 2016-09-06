CREATE TABLE [dbo].[NotificationMessages] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [NotIFicationType] NVARCHAR (255)   NULL,
    [Subject]          NVARCHAR (255)   NULL,
    [Body]             NVARCHAR (255)   NULL,
    [ReferenceId]      UNIQUEIDENTIFIER NULL,
    [SenderId]         UNIQUEIDENTIFIER NULL,
	[CreatedBy] UNIQUEIDENTIFIER NULL,
	[CreatedOn] DATETIME NULL DEFAULT GETDATE(), 
	[UpdatedBy] UNIQUEIDENTIFIER NULL, 
	[UpdatedOn] DATETIME NULL DEFAULT GETDATE(),
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NotificationMessagesSender_ToUsers] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Users] ([Id]), 
    CONSTRAINT [FK_NotificationMessagesCreated_ToUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_NotificationMessagesUpdated_ToUsers] FOREIGN KEY ([UpdatedBy]) REFERENCES [Users]([Id])
);

