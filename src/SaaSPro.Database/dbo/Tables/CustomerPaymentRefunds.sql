CREATE TABLE [dbo].[CustomerPaymentRefunds](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[TransactionId] [nvarchar](50) NOT NULL,
	[ChargeId] [nvarchar](50) NOT NULL,
	[Amount] [money] NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerPaymentRefunds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]