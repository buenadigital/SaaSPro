CREATE TABLE [dbo].[Plans] (
    [Id]         UNIQUEIDENTIFIER CONSTRAINT [DF_Plans_Id] DEFAULT (newid()) NOT NULL,
    [Name]       NVARCHAR (64)    NOT NULL,
    [Price]      MONEY            NOT NULL,
    [Period]     NVARCHAR (50)    NOT NULL,
    [OrderIndex] INT              CONSTRAINT [DF_Plans_OrderIndex] DEFAULT ((0)) NOT NULL,
    [PlanCode]   NVARCHAR (64)    NULL,
    [Enabled]    BIT              NULL,
	[CreatedBy] UNIQUEIDENTIFIER NULL,
	[CreatedOn] DATETIME NULL DEFAULT GETDATE(), 
	[UpdatedBy] UNIQUEIDENTIFIER NULL, 
	[UpdatedOn] DATETIME NULL DEFAULT GETDATE(),
    CONSTRAINT [PK_Plans_1] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_PlansCreated_ToUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_PlansUpdated_ToUsers] FOREIGN KEY ([UpdatedBy]) REFERENCES [Users]([Id])
);



