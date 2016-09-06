CREATE TABLE [dbo].[ReferenceLists] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [SystemName] NVARCHAR (255)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([SystemName] ASC)
);

