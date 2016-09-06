CREATE TABLE [dbo].[RoleUsers] (
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_RolesUsers_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]),
    CONSTRAINT [FK_RolesUsers_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

