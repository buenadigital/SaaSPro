CREATE TABLE [dbo].[PlanInfo] (
    [Id]         UNIQUEIDENTIFIER CONSTRAINT [DF_PlanInfoItems_Id] DEFAULT (newid()) NOT NULL,
    [Name]       NVARCHAR (64)    NOT NULL,
    [OrderIndex] INT              CONSTRAINT [DF_PlanOptions_OrderIndex] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PlanInfoItems] PRIMARY KEY CLUSTERED ([Id] ASC)
);

