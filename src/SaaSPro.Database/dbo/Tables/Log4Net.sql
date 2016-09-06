CREATE TABLE [dbo].[Log](
   [LogId] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [varchar](255) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Logger] [varchar](255) NOT NULL,
	[Message] [varchar](4000) NOT NULL,
	[Exception] [varchar](2000) NULL, 
    CONSTRAINT [PK_Log] PRIMARY KEY ([LogId])
) ON [PRIMARY]