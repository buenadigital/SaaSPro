CREATE TABLE [dbo].[Roles] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [Name]       NVARCHAR (255)   NULL,
    [UserType]   NVARCHAR (255)   NULL,
    [SystemRole] BIT              NULL,
    [CustomerId]   UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Roles_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

