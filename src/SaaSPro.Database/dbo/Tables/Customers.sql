CREATE TABLE [dbo].[Customers] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [FullName]          NVARCHAR (50)    NOT NULL,
    [Hostname]          NVARCHAR (128)   NOT NULL,
    [Company]           NVARCHAR (50)    NULL,
    [Enabled]           BIT              NOT NULL,
    [EncryptionKey]     NVARCHAR (255)   NOT NULL,
    [PaymentCustomerId] NVARCHAR (64)    NULL,
    [PlanId]            UNIQUEIDENTIFIER NULL,
    [AdminUserId]       UNIQUEIDENTIFIER NULL,
	[PlanCreatedOn]		DATETIME NULL,
	[PlanUpdatedOn]		DATETIME NULL,
	[PlanCanceledOn]	DATETIME NULL,
	[CreatedBy] UNIQUEIDENTIFIER NULL,
	[CreatedOn] DATETIME NULL DEFAULT GETDATE(), 
	[UpdatedBy] UNIQUEIDENTIFIER NULL, 
	[UpdatedOn] DATETIME NULL DEFAULT GETDATE(),
    CONSTRAINT [PK_dbo.CustomerDescriptions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Customers_Plans] FOREIGN KEY ([PlanId]) REFERENCES [dbo].[Plans] ([Id]),
    CONSTRAINT [FK_Customers_Users] FOREIGN KEY ([AdminUserId]) REFERENCES [dbo].[Users] ([Id]), 
    CONSTRAINT [FK_CustomersCreated_ToUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_CustomersUpdated_ToUsers] FOREIGN KEY ([UpdatedBy]) REFERENCES [Users]([Id])
);

