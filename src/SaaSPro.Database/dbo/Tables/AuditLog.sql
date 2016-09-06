CREATE TABLE [dbo].[AuditLog] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [TimeStamp]   DATETIME         NOT NULL,
    [Username]    NVARCHAR (255)   NULL,
    [EntityType]  NVARCHAR (255)   NOT NULL,
    [EntityId]    UNIQUEIDENTIFIER NOT NULL,
    [Action]      NVARCHAR (255)   NOT NULL,
    [Description] NVARCHAR (MAX)   NULL,
    [CustomerId]    UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuditLog_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

