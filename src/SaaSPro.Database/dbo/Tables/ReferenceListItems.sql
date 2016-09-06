CREATE TABLE [dbo].[ReferenceListItems] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [Value]           NVARCHAR (255)   NOT NULL,
    [ReferenceListId] UNIQUEIDENTIFIER NULL,
    [CustomerId]        UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReferenceListItems_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ReferenceListItems_ToReferenceLists] FOREIGN KEY ([ReferenceListId]) REFERENCES [dbo].[ReferenceLists] ([Id])
);

