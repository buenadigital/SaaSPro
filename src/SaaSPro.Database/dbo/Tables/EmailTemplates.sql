CREATE TABLE [dbo].[EmailTemplates](
	[Id] [uniqueidentifier] NOT NULL,
	[TemplateName] [nvarchar](50) NOT NULL,
	[Template] [text] NULL,
	[FromEmail] [nvarchar](100) NULL,
	[Subject] [nvarchar](max) NULL,
	[Body] [text] NULL,
	[TimeStamp] [datetime] NULL,
	[CreatedBy] UNIQUEIDENTIFIER NULL,
	[CreatedOn] DATETIME NULL DEFAULT GETDATE(), 
	[UpdatedBy] UNIQUEIDENTIFIER NULL, 
	[UpdatedOn] DATETIME NULL DEFAULT GETDATE(), 
    CONSTRAINT [FK_EmailTemplatesCreated_ToUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_EmailTemplatesUpdated_ToUsers] FOREIGN KEY ([UpdatedBy]) REFERENCES [Users]([Id])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];