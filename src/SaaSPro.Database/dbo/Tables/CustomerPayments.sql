CREATE TABLE [dbo].[CustomerPayments](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[TransactionId] [nvarchar](50) NOT NULL,
	[Amount] [money] NOT NULL,
	[Refunded] [bit] NOT NULL,
	[CreatedBy] UNIQUEIDENTIFIER NULL,
	[CreatedOn] DATETIME NULL DEFAULT GETDATE(), 
	[UpdatedBy] UNIQUEIDENTIFIER NULL, 
	[UpdatedOn] DATETIME NULL DEFAULT GETDATE(),
 CONSTRAINT [PK_CustomerPayments] PRIMARY KEY CLUSTERED 
([Id] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY], 
    CONSTRAINT [FK_CustomerPaymentsCreated_ToUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_CustomerPaymentsUpdated_ToUsers] FOREIGN KEY ([UpdatedBy]) REFERENCES [Users]([Id])
) ON [PRIMARY]