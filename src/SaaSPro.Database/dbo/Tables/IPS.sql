CREATE TABLE [dbo].[IPS] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]  DATETIME         NULL,
    [UpdatedOn]  DATETIME         NULL,
    [Name]       NVARCHAR (255)   NULL,
    [StartBytes] VARBINARY (MAX)  NOT NULL,
    [EndBytes]   VARBINARY (MAX)  NOT NULL,
    [CreatedBy]  UNIQUEIDENTIFIER NULL,
    [UpdatedBy]  UNIQUEIDENTIFIER NULL,
    [CustomerId]   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK__IPS__3214EC077279D204] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IPS_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_IPSUpdated_ToUsers] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [FK_IPSCreated_ToUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([Id])
);

