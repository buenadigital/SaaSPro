CREATE TABLE [dbo].[PlanInfoValues] (
    [PlanId]         UNIQUEIDENTIFIER NOT NULL,
    [PlanInfoItemId] UNIQUEIDENTIFIER NOT NULL,
    [Value]          NVARCHAR (64)    NOT NULL,
    CONSTRAINT [PK_PlanInfoItemValues] PRIMARY KEY CLUSTERED ([PlanId] ASC, [PlanInfoItemId] ASC)
);

