CREATE TABLE [dbo].[Projects] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [Name]          NVARCHAR (50)    NOT NULL,
    [CustomerId]          UNIQUEIDENTIFIER NOT NULL,
	[CreatedBy] UNIQUEIDENTIFIER NULL,
	[CreatedOn] DATETIME NULL DEFAULT GETDATE(), 
	[UpdatedBy] UNIQUEIDENTIFIER NULL, 
	[UpdatedOn] DATETIME NULL DEFAULT GETDATE(),
    CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Projects_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]), 
    CONSTRAINT [FK_ProjectsCreated_ToUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_ProjectsUpdated_ToUsers] FOREIGN KEY ([UpdatedBy]) REFERENCES [Users]([Id])
);

