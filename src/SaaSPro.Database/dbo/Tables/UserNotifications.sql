CREATE TABLE [dbo].[UserNotifications] (
    [UserId]    UNIQUEIDENTIFIER NOT NULL,
    [MessageId] UNIQUEIDENTIFIER NOT NULL,
    [HasRead]   BIT              NULL,
	[CreatedBy] UNIQUEIDENTIFIER NULL,
	[CreatedOn] DATETIME NULL DEFAULT GETDATE(), 
	[UpdatedBy] UNIQUEIDENTIFIER NULL, 
	[UpdatedOn] DATETIME NULL DEFAULT GETDATE(),
    PRIMARY KEY CLUSTERED ([UserId] ASC, [MessageId] ASC),
    CONSTRAINT [FK_UserNotificationsMessageId_ToNotificationMessages] FOREIGN KEY ([MessageId]) REFERENCES [dbo].[NotificationMessages] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserNotificationsUserId_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserNotificationsCreated_ToUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_UserNotificationsUpdated_ToUsers] FOREIGN KEY ([UpdatedBy]) REFERENCES [Users]([Id])
);

